using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.Modifiers;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.StockLogic;
using TMGmod.NY;
#if FEATURE_EDITOR_SKINS
using System.Collections.Generic;
using System.Linq;
#endif

namespace TMGmod.Core.WClasses
{
    public abstract class BaseGun : Gun, ISupportEnablingSkins
    {
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
            ToPrevKforce = true;
            _baseActiveModifier = DefaultModifier();
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

        protected bool IntrinsicAccuracy { get; set; }

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

        protected virtual float CalculateKforce(float kickForce)
        {
            return kickForce;
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
            OnFire();
            MaybeAddNyCase();
        }

        protected virtual void RealFire()
        {
            base.Fire();
        }

        private void FireWithKforce()
        {
            var previousAmmo = ammo;
            RealFire();
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

        protected virtual float CalculateAccuracy(float accuracy)
        {
            return accuracy;
        }

        private void SetAccuracy()
        {
            if (_ammoType != null && !IntrinsicAccuracy) _ammoType.accuracy = Accuracy;
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

        protected virtual bool DynamicAccuracy()
        {
            return !IntrinsicAccuracy;
        }

        protected virtual bool DynamicKforce()
        {
            return true;
        }

        protected virtual bool DynamicFeatures()
        {
            return true;
        }

        private void UpdateInternals()
        {
            if (DynamicAccuracy())
                SetAccuracy();
            UpdateHone();
        }

        private void UpdateFeatures()
        {
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
            value = (value % m + m) % m;
            sm.frame = value;
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

        public override void EditorPropertyChanged(object property)
        {
            if (this is IHaveAllowedSkins target) target.UpdateSkin();

            base.EditorPropertyChanged(property);
        }

        public Gun AsAGun()
        {
            return this;
        }

        protected virtual void BaseOnFire()
        {
        }

        protected virtual void OnFire()
        {
            ActiveModifier.ModifyFire(BaseOnFire);
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

            protected override void ModifyFire()
            {
            }

            protected override void ModifyUpdate()
            {
                _target.DynamicOnUpdate();
            }

            public override float ModifyAccuracy(float accuracy)
            {
                return _target.CalculateAccuracy(accuracy);
            }

            public override float ModifyKforce(float kforce)
            {
                return _target.CalculateKforce(kforce);
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

            return contextMenu;
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
            var v = OffsetLocal(new Vec2(64, 0));
            Graphics.DrawLine(barrelPosition, barrelPosition + v.Rotate(a, Vec2.Zero), Color.Red);
            Graphics.DrawLine(barrelPosition, barrelPosition + v.Rotate(-a, Vec2.Zero), Color.Red);
        }

        private void DrawDamage()
        {
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
    }
}
