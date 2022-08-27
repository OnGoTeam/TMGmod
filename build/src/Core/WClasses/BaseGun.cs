using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.Modifiers;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.SkinLogic;
using TMGmod.NY;
using System.Linq;
using TMGmod.Core.DamageLogic;
using TMGmod.Core.Modifiers.Pipelining;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.Modifiers.Updating;

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

        private void SetSkin(int skin)
        {
            FrameId = FrameId / SkinFrames * SkinFrames + skin.Modulo(SkinFrames);
        }

        protected int NonSkin
        {
            get => FrameId / SkinFrames;
            set => SetNonSkin(value);
        }

        public int Skin
        {
            get => FrameId % SkinFrames;
            set => SetSkin(value);
        }

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
                2f; //значение указано в процентах. Вне праздников - 0,1%, во время праздников - 2%, до 1.2 оставить 0,5%

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
            ActiveModifier = DefaultModifier();
        }

        protected void SetAmmoType<T>() where T : AmmoType, new()
        {
            SetAmmoType(new T());
        }

        protected void SetAmmoType(AmmoType at)
        {
            _ammoType = at;
            MaxAccuracy = _ammoType.accuracy;
            _bulletColor = _ammoType.bulletColor;
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
            duck is null ? new Vec2(0, 0) : !(duck.sliding || duck.crouch) ? new Vec2(0, 0) : new Vec2(0, 1);

        [UsedImplicitly]
        protected Vec2 HoldOffsetNoExtra
        {
            get => _holdOffset - ExtraHoldOffset;
            set => _holdOffset = value + ExtraHoldOffset;
        }

        private IModifyEverything ActiveModifier { get; set; }

        protected virtual float BaseAccuracy => MaxAccuracy;

        private float Accuracy => ClipAccuracy(ActiveModifier.ModifyAccuracy(BaseAccuracy));

        protected virtual float BaseKforce => _kickForce;
        protected virtual float StatsKforce => BaseKforce;

        private float Kforce => Math.Max(0f, ActiveModifier.ModifyKforce(BaseKforce));

        [UsedImplicitly]
        public BitBuffer ModifierBuffer
        {
            get
            {
                var buffer = new BitBuffer();
                buffer.Write(true);
                ActiveModifier.Write(buffer, () => buffer.Write(FrameId));
                return buffer;
            }
            set
            {
                if (value.ReadBool())
                    ActiveModifier.Read(value, () => FrameId = value.ReadInt());
            }
        }

        [UsedImplicitly] public StateBinding MbBinding = new(nameof(ModifierBuffer));

        public int SkinValue
        {
            get => _skinValue;
            set
            {
                _skinValue = value;
                if (this is IHaveAllowedSkins target) target.UpdateSkin();
            }
        }

        private static IModifyEverything DefaultModifier() => Modifier.Identity();

        protected void ResetModifier() => ActiveModifier = Modifier.Identity();

        protected void Compose(params IModifyEverything[] modifiers) =>
            ActiveModifier = ActiveModifier.Compose(modifiers);

        protected void ComposeSilencer(Func<bool> get, Action<bool> update)
        {
            var silencerProperty = new SynchronizedProperty<bool>(
                get,
                (old, value) =>
                {
                    if (value != old)
                        FrameUtils.SwitchedSilencer(old);
                    update(value);
#if DEBUG
                    _wait = Math.Max(_wait, _fireWait);
#endif
                }
            );
            Compose(
                silencerProperty,
                new Quacking(this, true, true, silencerProperty.Flip, "silencer", () => barrelOffset)
            );
        }

        protected void ComposeLaser(Action<bool> update)
        {
            var laserProperty = new SynchronizedProperty<bool>(
                () => laserSight,
                (old, value) =>
                {
                    if (value != old)
                        SFX.Play(GetPath("sounds/tuduc.wav"));
                    laserSight = value;
                    update(laserSight);
                }
            );
            Compose(
                laserProperty,
                new Quacking(this, true, true, laserProperty.Flip, "laser", () => laserOffset)
            );
        }

        private void SetKforce() => _kickForce = Kforce;

        private void AddNyCase() => Level.Add(new NewYearCase(x, y));

        private void MaybeAddNyCase()
        {
            if (Rando.Float(0f, 1f) < PresentChancePercentage / 100f) AddNyCase();
        }

        private void OnAmmoSpent()
        {
            OnSpent();
            MaybeAddNyCase();
        }

        protected virtual void RealFire() => base.Fire();

        private void ModifiedFire() => ActiveModifier.ModifyFire(RealFire);

        private void FireWithKforce()
        {
            var previousAmmo = ammo;
            ModifiedFire();
            if (ammo < previousAmmo)
                OnAmmoSpent();
        }

        protected virtual bool CanFire() => ActiveModifier.CanFire();

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
            SetAccuracy();
            FireWithDynamicKforce();
        }

        public override void Fire()
        {
            if (CanFire() || receivingPress)
                DoFire();
        }

        public void ForeignFire()
        {
            UpdateAction();
            Fire();
            _fireActivated = true;
            UpdateAction();
        }

        private float ClipAccuracy(float accuracy) => Maths.Clamp(accuracy, MinAccuracy, BaseAccuracy);

        private void SetAccuracy()
        {
            if (_ammoType is { }) _ammoType.accuracy = Accuracy;
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

        private void UpdateInternals()
        {
            SetAccuracy();
            UpdateHone();
        }

        private float _waitReturn; // intentionally not synchronized

        public override void Update()
        {
            UpdateInternals();
            OnUpdate();
            _waitReturn = Maths.Clamp(_waitReturn, 0f, .15f);
            if (_wait > _waitReturn)
            {
                _wait -= _waitReturn;
                if (_wait < .15f)
                    _waitReturn = .15f - _wait;
                else
                    _waitReturn = 0f;
            }
            else
            {
                _waitReturn -= _wait;
                _wait = 0f;
            }

            _waitReturn = Maths.Clamp(_waitReturn, 0f, .15f);
            base.Update();
#if DEBUG
            if (duck is { } && duck.inputProfile.Down("UP") && duck.inputProfile.Down("STRAFE"))
                _flareAlpha = 100f;
#endif
        }

        protected virtual void PopBaseShell()
        {
            if (_ammoType is BaseAmmoType baseAmmo)
                baseAmmo.PopShell(Offset(ShellOffset).x, Offset(ShellOffset).y, -offDir, AddShell);
            else
                _ammoType.PopShell(Offset(ShellOffset).x, Offset(ShellOffset).y, -offDir);
        }

#if DEBUG
        protected virtual void AddShell(EjectedShell shell)
#else
        protected void AddShell(EjectedShell shell)
#endif
        {
            shell.velocity = shell.velocity.Rotate(angle, Vec2.Zero);
            shell.graphic.flipH = offDir < 0;
            shell.spinAngle = Maths.RadToDeg(-angle);
            shell.depth = depth.Add(1);
            shell.velocity += owner?.velocity ?? velocity;
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

        protected bool BipodsQ() => BipodsImplementation.BipodsQ(this);

        protected bool HandleQ() => HandleImplementation.HandleQ(this);

        protected static void SetSpriteMapFrameId(SpriteMap sm, int value, int m) => sm.frame = value.Modulo(m);

        public override void Draw()
        {
#if DEBUG
            DrawDebug();
#endif
            DrawRandom();
        }

        protected virtual SpriteMap EditorSpriteMap() => _graphic as SpriteMap;

        private void DrawRandom()
        {
            if (
                Level.activeLevel is Editor
                && this is IHaveAllowedSkins target
                && EditorSpriteMap() is { } sprite
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
                base.Draw();
        }

        public override void EditorPropertyChanged(object property)
        {
            if (this is IHaveAllowedSkins target) target.UpdateSkin();

            base.EditorPropertyChanged(property);
        }

        protected virtual void BaseOnSpent()
        {
        }

        private void OnSpent()
        {
            ActiveModifier.ModifySpent(BaseOnSpent);
        }

        protected virtual void BaseOnUpdate()
        {
        }

        private void OnUpdate()
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
            SkinValue = SkinValue;
            base.Initialize();
        }

        public bool Quacked() => duck?.inputProfile.Pressed("QUACK") == true;

        public void UnQuack()
        {
            if (Quacked()) SFX.Play("quack", -1, duck.quackPitch);
        }

        public override ContextMenu GetContextMenu()
        {
            var contextMenu = base.GetContextMenu();
            switch (this)
            {
                case IHaveAllowedSkins target when EditorSpriteMap() is { } sprite:
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
            subMenu.greyOut = false;
            contextMenu.AddItem(subMenu);
            return contextMenu;
        }

        private static IEnumerable<string> DamageCharacteristics(IDamage damage)
        {
            yield return $"Damage: {damage.DamageMean}";
        }

        private static IEnumerable<string> AmmoTypeCharacteristics(AmmoType ammoType)
        {
            if (ammoType is IDamage damage)
                foreach (var characteristic in DamageCharacteristics(damage))
                    yield return characteristic;
            yield return $"Accuracy: {100 * ammoType.accuracy}";
            yield return $"Range: {ammoType.range / 16f}";
            yield return $"Bullet Speed: {ammoType.bulletSpeed / 16f * 60f}";
            yield return $"Penetration: {ammoType.penetration}";
        }
#if DEBUG
        protected virtual IEnumerable<string> BaseCharacteristics()
#else
        private IEnumerable<string> BaseCharacteristics()
#endif
        {
            if (_ammoType is { })
                foreach (var characteristic in AmmoTypeCharacteristics(_ammoType))
                    yield return characteristic;
            yield return $"Total Ammo: {ammo}";
            if (_fullAuto)
                yield return "Full-Auto";
            yield return $"Kickforce: {StatsKforce}";
            if (_fireWait > 0 && !_manualLoad)
                yield return $"RPM: {Math.Round(3600 / (_fireWait / .15f))}";
        }

        private IEnumerable<string> Characteristics()
        {
            return ActiveModifier.ModifyPipeline(new CharacteristicsPipeline(BaseCharacteristics()));
        }

#if DEBUG
        public static IEnumerable<string> StatsHeader()
        {
            yield return "editor name";
            yield return "Ammo";
            yield return "MaxAccuracy";
            yield return "Accuracy";
            yield return "MinAccuracy";
            yield return "KickForce";
            yield return "FireWait";
            yield return "Range";
        }

        public IEnumerable<string> StatsLine()
        {
            Initialize();
            yield return editorName;
            yield return $"{ammo}";
            yield return $"{MaxAccuracy}";
            yield return $"{Accuracy}";
            yield return $"{MinAccuracy}";
            yield return $"{StatsKforce}";
            yield return $"{_fireWait}";
            yield return _ammoType is null ? "" : $"{_ammoType.range}";
        }
#endif
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

        public class SkinMix : IShowSkins
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

            public ICollection<int> AllowedSkins => _target.AllowedSkins;
            public SpriteMap SpriteBase { get; }

            public int SkinValue
            {
                get => _target.SkinValue;
                set => _target.SkinValue = value;
            }

            public int Skin
            {
                get => _target.Skin;
                set => _target.Skin = value;
            }
        }

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
            if (Network.isActive || ammoType is null) return;
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

        private static readonly Dictionary<Tuple<Type, string>, bool> Hints = new();

        private int _framesToHint;

        private void Hint(string hint, Func<Vec2> offset, Func<IEnumerable<Sprite>> image)
        {
            if (!isServerForObject || hint is null) return;
            var tuple = new Tuple<Type, string>(GetType(), hint);
#if DEBUG
            if (duck.inputProfile.Pressed("STRAFE"))
            {
                Hints.Clear();
                _framesToHint = 0;
            }
#endif
            if (Hints.ContainsKey(tuple)) return;
            if (_framesToHint > 0)
            {
                --_framesToHint;
                return;
            }

            _framesToHint = 300;
            Hints[tuple] = true;
            Level.Add(new HintThing(this, offset, hint, image()));
        }

        public void Hint(string hint, Func<Vec2> offset, params string[] trigger)
        {
            if (duck is { })
                Hint(hint, offset, () => trigger.Select(duck.inputProfile.GetTriggerImage));
        }

        private class HintThing : Thing
        {
            private readonly Thing _target;
            private readonly Func<Vec2> _offset;
            private readonly string _hint;
            private readonly IEnumerable<Sprite> _images;
            private int _ticks;

            public HintThing(Thing target, Func<Vec2> offset, string hint, IEnumerable<Sprite> images)
            {
                _target = target;
                _offset = offset;
                _hint = hint;
                _images = images.ToArray();
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

                foreach (var image in _images)
                {
                    image.depth = newDepth;
                    Graphics.Draw(image, pos.x, pos.y);
                    pos.x += image.width;
                }

                pos.y += Graphics.GetStringHeight(_hint) / 2f;
                Graphics.DrawStringOutline(_hint, pos, Color.White, depth: newDepth, outline: Color.Black);
            }
        }

        private Tex2D _laserTex;
        protected virtual Color LaserColor => Color.Red;

        public override void DoUpdate()
        {
            if (laserSight)
                _laserTex ??= Content.Load<Tex2D>("pointerLaser");

            base.DoUpdate();
        }

        public override void DrawGlow()
        {
            if (laserSight && held && _laserTex is { } && _wallPoint != Vec2.Zero)
            {
                var num = 1f;
                if (!Options.Data.fireGlow)
                    num = 0.4f;
                var p1 = Offset(laserOffset + new Vec2(0f, 0f));
                var length = (p1 - _wallPoint).length;
                var laserRange = 100f;
                if (ammoType is { })
                    laserRange = ammoType.range + (_barrelOffsetTL - _laserOffsetTL).x + ammoType.bulletSpeed / 2;
                var normalized = (_wallPoint - p1).normalized;
                p1 -= normalized.Rotate(Maths.PI / 2, Vec2.Zero) * .25f;
                var vec2 = p1 + normalized * Math.Min(laserRange, length);
                Graphics.DrawTexturedLine(_laserTex, p1, vec2, LaserColor * num, .5f, depth - 1);
                if ((double)length > laserRange)
                {
                    for (var index = 1; index < 4; ++index)
                    {
                        Graphics.DrawTexturedLine(_laserTex, vec2, vec2 + normalized * 2f,
                            LaserColor * ((float)(1.0 - index * 0.20000000298023224) * num), .5f, depth - 1);
                        vec2 += normalized * 2f;
                    }
                }

                if (_sightHit is { } && (double)length < laserRange)
                {
                    _sightHit.alpha = num;
                    _sightHit.color = LaserColor * num;
                    Graphics.Draw(_sightHit, _wallPoint.x, _wallPoint.y);
                }
            }

            var ls = laserSight;
            laserSight = false;
            base.DrawGlow();
            laserSight = ls;
        }
    }
}
