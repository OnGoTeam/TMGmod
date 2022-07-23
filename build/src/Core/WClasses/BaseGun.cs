using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.DamageLogic;
using TMGmod.Core.Modifiers;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.StockLogic;
using TMGmod.NY;
#if FEATURE_EDITOR_SKINS
using System.Linq;
#endif

namespace TMGmod.Core.WClasses
{
    public abstract class BaseGun : Gun, ISupportEnablingSkins, IHaveFrameId
    {
        protected int NonSkinFrames = 1;
        protected int SkinFrames = 10;
        private SpriteMap _smap;

        protected SpriteMap Smap
        {
            get => _smap;
            set => _graphic = _smap = value;
        }

        private void SetNonSkin(int nonSkin)
        {
            FrameId = FrameId % SkinFrames + SkinFrames * nonSkin;
        }

        protected int NonSkin
        {
            get => FrameId / SkinFrames;
            set => SetNonSkin(value);
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        public int FrameId
        {
            get => Smap.frame;
            set
            {
                SetSpriteMapFrameId(Smap, value, SkinFrames * NonSkinFrames);
                UpdateFrameId(value);
            }
        }

        protected virtual void UpdateFrameId(int frameId)
        {
        }

        private const float
            PresentChancePercentage =
                0.5f; //значение указано в процентах. Вне праздников - 0,1%, во время праздников - 2%, до 1.2 оставить 0,5%

        private IModifyEverything _baseActiveModifier;

        private bool _currHoneInit;
        private int _skinValue;
        protected Vec2 CurrHone;
        protected float MaxAccuracy = 1f;
        protected float MinAccuracy;

        [UsedImplicitly] protected float PrevKforce;

        protected Vec2 ShellOffset;

        [UsedImplicitly] protected bool ToPrevKforce;

        protected BaseGun(float xval, float yval) : base(xval, yval)
        {
            _graphic = Smap;
            _type = "gun";
            ToPrevKforce = true;
            _baseActiveModifier = DefaultModifier();
        }

        protected void SetAmmoType<T>() where T : AmmoType, new()
        {
            SetAmmoType(new T());
        }

        protected void SetAmmoType(AmmoType at)
        {
            _ammoType = at;
            MaxAccuracy = _ammoType.accuracy;
        }

        protected void SetAmmoType<T>(float maxAccuracy) where T : AmmoType, new()
        {
            _ammoType = new T();
            MaxAccuracy = maxAccuracy;
        }

        protected void ComposeFirstAccuracy(uint delay)
        {
            var delta = MaxAccuracy - _ammoType.accuracy;
            Compose(new FirstAccuracy(delay, accuracy => accuracy - delta));
        }

        protected void ComposeSimpleBurst(int num, float wait)
        {
            Compose(new Burst(this, true, num, wait));
        }

        [UsedImplicitly]
        protected Vec2 ExtraHoldOffset =>
            duck == null ? new Vec2(0, 0) : !duck.sliding ? new Vec2(0, 0) : new Vec2(0, 1);

        [UsedImplicitly]
        protected Vec2 HoldOffsetNoExtra
        {
            get => _holdOffset - ExtraHoldOffset;
            set => _holdOffset = value + ExtraHoldOffset;
        }

        protected virtual IModifyEverything ActiveModifier => _baseActiveModifier;

        protected virtual float BaseAccuracy => MaxAccuracy;

        protected virtual float Accuracy => ClipAccuracy(ActiveModifier.ModifyAccuracy(BaseAccuracy));

        protected virtual float BaseKforce => _kickForce;

        protected virtual float Kforce => Math.Max(0f, ActiveModifier.ModifyKforce(BaseKforce));

        [UsedImplicitly]
        public BitBuffer ModifierBuffer
        {
            get => GetBuffer(ActiveModifier, () => { });
            set => ActiveModifier.Read(value, () => { });
        }

        [UsedImplicitly] public StateBinding MbBinding { get; } = new StateBinding(nameof(ModifierBuffer));

        public int SkinValue
        {
            get => _skinValue;
            set
            {
                _skinValue = value;
                if (this is IHaveAllowedSkins target) target.UpdateSkin();
            }
        }

        private IModifyEverything DefaultModifier()
        {
            return new BaseModifier(this);
        }

        protected void ResetModifier()
        {
            _baseActiveModifier = Modifier.Identity();
        }

        protected void Compose(params IModifyEverything[] modifiers)
        {
            _baseActiveModifier = _baseActiveModifier.Compose(modifiers);
        }

        private void SetKforce()
        {
            _kickForce = Kforce;
        }

        private void AddNyCase()
        {
            Level.Add(new NewYearCase(x, y));
        }

        private void MaybeAddNyCase()
        {
            if (Rando.Float(0f, 1f) < PresentChancePercentage / 100f) AddNyCase();
        }

        private void OnAmmoSpent()
        {
            OnSpent();
            MaybeAddNyCase();
        }

        protected virtual void RealFire()
        {
            base.Fire();
        }

        protected virtual void ModifiedFire()
        {
            ActiveModifier.ModifyFire(RealFire);
        }

        private void FireWithKforce()
        {
            var previousAmmo = ammo;
            ModifiedFire();
            if (ammo < previousAmmo)
                OnAmmoSpent();
        }

        protected virtual bool CanFire()
        {
            switch (this)
            {
                case IHaveBipodState target when !target.BipodsDeployed() && !target.BipodsFolded():
                    return false;
                case IHaveStock target when !target.StockDeployed() && !target.StockFolded():
                    return false;
                default:
                    return true;
            }
        }

        private void SetKforceAndFire()
        {
            SetKforce();
            FireWithKforce();
        }

        private void FireWithDynamicKforce()
        {
            PrevKforce = _kickForce;
            SetKforceAndFire();
            if (ToPrevKforce)
                _kickForce = PrevKforce;
        }

        private void DoFire()
        {
            if (DynamicAccuracy())
                SetAccuracy();
            if (DynamicKforce())
                FireWithDynamicKforce();
            else
                FireWithKforce();
        }

        public override void Fire()
        {
            if (CanFire()) DoFire();
        }

        private float ClipAccuracy(float accuracy)
        {
            return Maths.Clamp(accuracy, MinAccuracy, BaseAccuracy);
        }

        private void SetAccuracy()
        {
            if (_ammoType != null) _ammoType.accuracy = Accuracy;
        }

        private void UpdateHone()
        {
            if (!_currHoneInit)
            {
                _currHoneInit = true;
                CurrHone = _holdOffset;
            }

            HoldOffsetNoExtra = CurrHone;
            CurrHone = HoldOffsetNoExtra;
        }

        protected virtual bool DynamicAccuracy() => true;

        protected virtual bool DynamicKforce() => true;

        protected virtual bool DynamicFeatures() => true;

        private void UpdateInternals()
        {
            if (DynamicAccuracy())
                SetAccuracy();
            UpdateHone();
        }

        private void UpdateFeatures()
        {
            Hint();
            switch (this)
            {
                case ICanDisableBipods target:
                    target.UpdateSwitchableBipods();
                    break;
                case IHaveBipods target:
                    target.UpdateBipods();
                    break;
                case IHaveStock target:
                    target.UpdateStock();
                    break;
            }
        }

        public override void Update()
        {
            UpdateInternals();
            OnUpdate();
            base.Update();
        }

        protected virtual void PopBaseShell()
        {
            if (_ammoType is BaseAmmoType baseAmmo)
                baseAmmo.PopShell(Offset(ShellOffset).x, Offset(ShellOffset).y, -offDir, AddShell);
            else
                _ammoType.PopShell(Offset(ShellOffset).x, Offset(ShellOffset).y, -offDir);
        }

        protected virtual void AddShell(EjectedShell shell)
        {
            shell.velocity = shell.velocity.Rotate(angle, Vec2.Zero);
            Level.Add(shell);
        }

        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell) PopBaseShell();
                --ammo;
            }

            loaded = true;
        }

        protected bool BipodsQ(bool bypassihb = false)
        {
            return BipodsImplementation.BipodsQ(this, bypassihb);
        }

        protected bool HandleQ()
        {
            return BaseGunImplementations.HandleQ(this);
        }

        [PublicAPI]
        public static void SetSpriteMapFrameId(SpriteMap sm, int value, int m)
        {
            sm.frame = (value % m + m) % m;
        }

        public override void Draw()
        {
#if DEBUG
            DrawDebug();
#endif
#if FEATURE_EDITOR_SKINS
            DrawRandom();
#else
            base.Draw();
#endif
        }


#if FEATURE_EDITOR_SKINS
        private void DrawRandom()
        {
            if (
                Level.activeLevel is Editor
                && this is IHaveAllowedSkins target
                && _graphic is SpriteMap sprite
                && target.SkinValue == -1
            )
            {
                _flipHorizontal = offDir <= 0;
                ContextSkinRender.WithRandomized(
                    new SkinMix(target, sprite),
                    s =>
                    {
                        var old = _graphic;
                        _graphic = s;
                        base.Draw();
                        _graphic = old;
                    }
                );
            }
            else
            {
                base.Draw();
            }
        }
#endif

        public override void EditorPropertyChanged(object property)
        {
            if (this is IHaveAllowedSkins target) target.UpdateSkin();

            base.EditorPropertyChanged(property);
        }

        public BaseGun AsAGun()
        {
            return this;
        }

        protected virtual void BaseOnSpent()
        {
        }

        protected virtual void OnSpent()
        {
            ActiveModifier.ModifySpent(BaseOnSpent);
        }

        protected virtual void BaseOnUpdate()
        {
        }

        private void DynamicOnUpdate()
        {
            if (DynamicFeatures())
                UpdateFeatures();
        }

        protected virtual void OnUpdate()
        {
            ActiveModifier.ModifyUpdate(BaseOnUpdate);
        }

        protected virtual void OnInitialize()
        {
            SetAccuracy();
        }


        public override void Initialize()
        {
            OnInitialize();
            base.Initialize();
        }

        private static BitBuffer GetBuffer(ISync modifier, Action write)
        {
            var buffer = new BitBuffer();
            modifier.Write(buffer, write);
            return buffer;
        }

        private class BaseModifier : Modifier
        {
            private readonly BaseGun _target;

            public BaseModifier(BaseGun target)
            {
                _target = target;
            }

            protected override void ModifyUpdate()
            {
                _target.DynamicOnUpdate();
            }
        }

#if FEATURE_EDITOR_SKINS
        public override ContextMenu GetContextMenu()
        {
            var contextMenu = base.GetContextMenu();
            switch (this)
            {
                case IHaveAllowedSkins target when _graphic is SpriteMap sprite:
                {
                    foreach (var skin in target.AllowedSkins.Concat(new[] { -1 }))
                        contextMenu.AddItem(new ContextSkinRender(new SkinMix(target, sprite), skin));
                    break;
                }
            }

            ContextMenu subMenu = new EditorGroupMenu(contextMenu) { text = "Characteristics" };
            foreach (var characteristic in Characteristics())
                subMenu.AddItem(
                    new ContextMenu(subMenu)
                    {
                        text = characteristic,
                        itemSize = new Vec2(Graphics.GetStringWidth(characteristic) + 8f, 16f),
                    }
                );
            contextMenu.AddItem(subMenu);
            return contextMenu;
        }

        private IEnumerable<string> Characteristics()
        {
            var chars = new List<string>();
            if (_ammoType != null) chars.Add($"Accuracy: {100 * _ammoType.accuracy}");
            if (_ammoType != null) chars.Add($"Range: {_ammoType.range / 16f}");
            if (_ammoType != null) chars.Add($"Bullet Speed: {_ammoType.bulletSpeed / 16f * 60f}");
            if (_ammoType != null) chars.Add($"Penetration: {_ammoType.penetration}");
            chars.Add($"Kickforce: {_kickForce}");
            if (_fireWait > 0 && !_manualLoad && _fullAuto) chars.Add($"RPM: {Math.Round(3600 / (_fireWait / .15f))}");
            return chars;
        }

        public override BinaryClassChunk Serialize()
        {
            var node = base.Serialize();
            if (this is IHaveSkin)
                node.AddProperty("skin", _skinValue);
            return node;
        }

        public override bool Deserialize(BinaryClassChunk node)
        {
            if (this is IHaveSkin)
                _skinValue = node.GetProperty<int>("skin");
            return base.Deserialize(node);
        }

        private class SkinMix : IShowSkins
        {
            private readonly IHaveAllowedSkins _target;

            public SkinMix(IHaveAllowedSkins target, SpriteMap sprite)
            {
                _target = target;
                SpriteBase = sprite;
            }

            public int FrameId
            {
                get => _target.FrameId;
                set => _target.FrameId = value;
            }

            public StateBinding FrameIdBinding => _target.FrameIdBinding;
            public ICollection<int> AllowedSkins => _target.AllowedSkins;
            public SpriteMap SpriteBase { get; }

            public int SkinValue
            {
                get => _target.SkinValue;
                set => _target.SkinValue = value;
            }
        }
#endif
#if DEBUG
        private void DrawAccuracy()
        {
            if (ammoType is null) return;
            var a = (1 - ammoType.accuracy) / 2;
            var v = barrelVector * 64;
            v = v.Rotate(offDir * Maths.DegToRad(ammoType.barrelAngleDegrees), Vec2.Zero);
            Graphics.DrawLine(barrelPosition, barrelPosition + v.Rotate(a, Vec2.Zero), Color.Red);
            Graphics.DrawLine(barrelPosition, barrelPosition + v.Rotate(-a, Vec2.Zero), Color.Red);
        }

        private void DrawDamage()
        {
            if (Network.isActive) return;
            if (ammoType is null) return;
            var start = barrelPosition + new Vec2(0, -64);
            var x1 = OffsetLocal(new Vec2(ammoType.range, 0));
            var y1 = new Vec2(0, -64);
            var last = start + 0 * x1 + 1 * y1;
            for (var i = 0; i < 100; ++i)
            {
                var q = (i + 1) / 100f;
                var next = start + q * x1 + DamageImplementation.CalculateCoeff(ammoType, q) * y1;
                Graphics.DrawLine(last, next, Color.Lime);
                last = next;
            }

            Graphics.DrawLine(start, start + x1, Color.MediumPurple);
            Graphics.DrawLine(start, start + y1, Color.MediumPurple);
        }

        private void DrawDebug()
        {
            if (Level.activeLevel is Editor) return;
            if (duck is null) return;
            DrawAccuracy();
            DrawDamage();
        }
#endif

        private static readonly Dictionary<Tuple<Type, string>, bool> Hints =
            new Dictionary<Tuple<Type, string>, bool>();

        private void Hint(string hint, Func<Vec2> offset, Func<Sprite> image)
        {
            if (!isServerForObject) return;
            if (hint is null) return;
            var tuple = new Tuple<Type, string>(GetType(), hint);
#if DEBUG
            if (Hints.ContainsKey(tuple) && !duck.inputProfile.Pressed("STRAFE")) return;
#else
            if (Hints.ContainsKey(tuple)) return;
#endif
            Hints[tuple] = true;
            Level.Add(new HintThing(this, offset, hint, image()));
        }

        public void Hint(string hint, Func<Vec2> offset, string trigger)
        {
            if (duck != null)
                Hint(hint, offset, () => duck.inputProfile.GetTriggerImage(trigger));
        }

        private void Hint()
        {
            if (NeedHint()) Hint(HintMessage, HintOffset, HintTrigger);
        }

        protected virtual string HintMessage
        {
            get
            {
                switch (this)
                {
                    case IDeployBipods target when target.BipodsDeployed():
                        return "bipods";
                    case IHaveStock _:
                        return "stock";
                    default:
                        return null;
                }
            }
        }

        private Vec2 HintOffset()
        {
            switch (this)
            {
                case IHaveStock _:
                    return new Vec2(-8f, 0f) - _holdOffset;
                default:
                    return barrelOffset;
            }
        }

        private const string HintTrigger = "QUACK";

        private bool NeedHint()
        {
            switch (this)
            {
                case IDeployBipods target when !target.BipodsDeployed():
                    return false;
                default:
                    return true;
            }
        }

        private class HintThing : Thing
        {
            private readonly Thing _target;
            private readonly Func<Vec2> _offset;
            private readonly string _hint;
            private readonly Sprite _image;
            private int _ticks;

            public HintThing(Thing target, Func<Vec2> offset, string hint, Sprite image)
            {
                _target = target;
                _offset = offset;
                _hint = hint;
                _image = image;
            }

            public override void Update()
            {
                ++_ticks;
                if (_ticks > 180)
                    Level.Remove(this);
            }

            public override void Draw()
            {
                var pos = _target.Offset(_offset());
                position = pos;
                var newDepth = _target.depth.Add(1);
                angle = _ticks * .07f;
                Graphics.DrawCircle(pos, 4f + (float)Math.Sin(_ticks * .1f), Color.Coral, depth: newDepth);
                const int n = 3;
                for (var i = 0; i < n; ++i)
                {
                    angle += (float)(2 * Math.PI / n);
                    Graphics.DrawLine(
                        Offset(new Vec2(2.5f, 0f)),
                        Offset(new Vec2(5.5f, 0f)),
                        Color.Coral,
                        depth: newDepth
                    );
                }

                pos.x += 16f;
                pos.y -= 8f;
                _image.depth = newDepth;
                Graphics.Draw(_image, pos.x, pos.y - _image.height / 2f);
                pos.y += _image.height / 2f;
                Graphics.DrawStringOutline(_hint, pos, Color.White, depth: newDepth, outline: Color.Black);
            }
        }
    }
}
