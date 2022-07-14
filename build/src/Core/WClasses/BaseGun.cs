using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.Modifiers;
using TMGmod.NY;
#if DEBUG
using TMGmod.Core.AmmoTypes;
#endif

namespace TMGmod.Core.WClasses
{
    public abstract class BaseGun : Gun
    {
        private const float
            PresentChancePercentage =
                0.5f; //значение указано в процентах. Вне праздников - 0,1%, во время праздников - 2%, до 1.2 оставить 0,5%

        private bool _currHoneInit;
        protected float BaseAccuracy = 1f;
        protected Vec2 CurrHone;
        protected float MinAccuracy;

        [UsedImplicitly] protected float PrevKforce;

        protected Vec2 ShellOffset;

        [UsedImplicitly] protected bool ToPrevKforce;

        protected BaseGun(float xval, float yval) : base(xval, yval)
        {
            ToPrevKforce = true;
            BaseActiveModifier = DefaultModifier = new BaseModifier(this);
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

        private float CalculateHSpeedKforce(IHspeedKforce target, float kickForce)
        {
            return duck != null
                ? Math.Abs(duck.hSpeed) < 0.1f ? target.KickForceSlowAr : target.KickForceFastAr
                : kickForce;
        }

        private static float CalculateRandKforce(IRandKforce target)
        {
            return Rando.Float(target.KickForce1Lmg, target.KickForce2Lmg);
        }

        private static float CalculateFirstKforce(IFirstKforce target, float kickForce)
        {
            return target.CurrentDelaySmg <= 0 ? kickForce + target.KickForceDeltaSmg : kickForce;
        }

        protected virtual float CalculateKforce(float kickForce)
        {
            switch (this)
            {
                case IHspeedKforce ihskf:
                    return CalculateHSpeedKforce(ihskf, kickForce);
                case IRandKforce irkf:
                    return CalculateRandKforce(irkf);
                case IFirstKforce ifkf:
                    return CalculateFirstKforce(ifkf, kickForce);
                default:
                    return kickForce;
            }
        }

        private void SetKforce() => _kickForce = Kforce();

        private void AddNyCase() => Level.Add(new NewYearCase(x, y));

        private void FireLoseAccuracy(ILoseAccuracy target)
        {
            ammoType.accuracy = ClipAccuracy(ammoType.accuracy - target.DrainAccuracyDmr);
        }

        private static void FireFirstPrecise(IFirstPrecise target)
        {
            target.CurrentDelayFp = target.MaxDelayFp;
        }

        private void FireAccuracy()
        {
            switch (this)
            {
                case ILoseAccuracy ila:
                    FireLoseAccuracy(ila);
                    break;
                case IFirstPrecise ifp:
                    FireFirstPrecise(ifp);
                    break;
            }
        }

        private static void FireFirstKforce(IFirstKforce target)
        {
            target.CurrentDelaySmg = target.MaxDelaySmg;
        }

        private void FireKforce()
        {
            switch (this)
            {
                case IFirstKforce ifk:
                    FireFirstKforce(ifk);
                    break;
            }
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

        protected virtual void RealFire() => base.Fire();

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
                case ISwitchBipods switching when switching.SwitchingBipods():
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

        private void DoFire()
        {
            PrevKforce = _kickForce;
            SetKforceAndFire();
            if (ToPrevKforce)
                _kickForce = PrevKforce;
        }

        public override void Fire()
        {
            if (CanFire()) DoFire();
        }

        private float ClipAccuracy(float accuracy)
        {
            return Maths.Clamp(accuracy, MinAccuracy, BaseAccuracy);
        }

        private static void UpdateFirstKforce(IFirstKforce target)
        {
            target.CurrentDelaySmg -= 1;
            if (target.CurrentDelaySmg < 0)
                target.CurrentDelaySmg = 0;
        }

        private void UpdateKforce()
        {
            switch (this)
            {
                case IFirstKforce ifkf:
                    UpdateFirstKforce(ifkf);
                    break;
            }
        }

        private float CalculateSpeedAccuracy(ISpeedAccuracy target)
        {
            return duck != null
                ? ClipAccuracy(
                    BaseAccuracy
                    +
                    target.SpeedAccuracyThreshold
                    -
                    (
                        Math.Abs(duck.hSpeed) * target.SpeedAccuracyHorizontal
                        +
                        Math.Abs(duck.vSpeed) * target.SpeedAccuracyVertical
                    )
                )
                : BaseAccuracy;
        }

        private float CalculateLoseAccuracy(ILoseAccuracy target)
        {
            return ClipAccuracy(ammoType.accuracy + target.RegenAccuracyDmr);
        }

        private float CalculateFirstPrecise(IFirstPrecise target)
        {
            return target.CurrentDelayFp <= 0f
                ? target.MaxAccuracyFp
                : BaseAccuracy;
        }

        private float CalculateAccuracy(float accuracy)
        {
            switch (this)
            {
                case ISpeedAccuracy isa:
                    return CalculateSpeedAccuracy(isa);
                case ILoseAccuracy ila:
                    return CalculateLoseAccuracy(ila);
                case IFirstPrecise ifp:
                    return CalculateFirstPrecise(ifp);
                default:
                    return accuracy;
            }
        }

        private void SetAccuracy()
        {
            if (_ammoType != null) _ammoType.accuracy = Accuracy();
        }

        private static void UpdateFirstPrecise(IFirstPrecise target)
        {
            target.CurrentDelayFp = Math.Max(target.CurrentDelayFp - 1, 0);
        }

        private void UpdateAccuracy()
        {
            switch (this)
            {
                case IFirstPrecise ifp:
                    UpdateFirstPrecise(ifp);
                    break;
            }
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

        private void UpdateFeatures()
        {
            if (this is ICanDisableBipods icdb)
                icdb.UpdateSwitchableBipods();
        }

        public override void Update()
        {
            UpdateInternals();
            OnUpdate();
            base.Update();
        }

        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell) _ammoType.PopShell(Offset(ShellOffset).x, Offset(ShellOffset).y, -offDir);
                --ammo;
            }

            loaded = true;
        }

        public static bool BipodsQ(Gun gun, bool bypassihb = false)
        {
            var duck = gun.duck;
            if (!bypassihb && gun is IHaveBipods ihb && ihb.BipodsDisabled) return false;
            return !(duck is null) && !gun.raised && (duck.crouch || duck.sliding) && duck.grounded &&
                   Math.Abs(duck.hSpeed) < 0.05f;
        }

        public static bool HandleQ(Gun gun)
        {
            var duck = gun.duck;
            return !(duck is null) && !gun.raised && duck.sliding && duck.grounded && Math.Abs(duck.hSpeed) < 1f;
        }

        [UsedImplicitly]
        public static bool SwitchStockQ(Gun gun)
        {
            var duck = gun.duck;
            return !(duck is null) && !duck.sliding;
        }

        protected bool BipodsQ(bool bypassihb = false) => BipodsQ(this, bypassihb);

        protected bool HandleQ() => HandleQ(this);

        protected bool SwitchStockQ() => SwitchStockQ(this);

        [PublicAPI]
        public static void SetSpriteMapFrameId(SpriteMap sm, int value, int m)
        {
            value = (value % m + m) % m;
            sm.frame = value;
        }
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
                var next = start + q * x1 + Damage.CalculateCoeff(ammoType, q) * y1;
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

        public override void Draw()
        {
            base.Draw();
#if DEBUG
            DrawDebug();
#endif
        }

        public override void EditorPropertyChanged(object property)
        {
            if (this is IHaveAllowedSkins iha)
            {
                iha.UpdateSkin();
            }

            base.EditorPropertyChanged(property);
        }

        public Gun AsAGun() => this;
        protected virtual float GetBaseAccuracy() => BaseAccuracy;
        protected virtual float Accuracy() => ActiveModifier.ModifyAccuracy(GetBaseAccuracy());
        protected virtual float GetBaseKforce() => _kickForce;

        protected virtual float Kforce() => ActiveModifier.ModifyKforce(GetBaseKforce());

        protected virtual void BaseOnFire()
        {
            FireAccuracy();
            FireKforce();
        }

        protected virtual void OnFire() => ActiveModifier.ModifyFire(BaseOnFire);

        protected virtual void BaseOnUpdate()
        {
            UpdateAccuracy();
            UpdateKforce();
            UpdateFeatures();
        }

        protected virtual void OnUpdate() => ActiveModifier.ModifyUpdate(BaseOnUpdate);
        protected IModifyEverything BaseActiveModifier;
        protected virtual IModifyEverything ActiveModifier => BaseActiveModifier;
        protected readonly IModifyEverything DefaultModifier;

        private class BaseModifier : Modifier
        {
            private readonly BaseGun _target;
            public BaseModifier(BaseGun target)
            {
                _target = target;
            }

            public override float ModifyAccuracy(float accuracy) => _target.CalculateAccuracy(accuracy);
            public override float ModifyKforce(float kforce) => _target.CalculateKforce(kforce);
        }
    }
}
