using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.NY;
#if DEBUG
using TMGmod.Core.AmmoTypes;
#endif

namespace TMGmod.Core.WClasses
{
    public abstract class BaseGun : Gun
    {
        private const float
            PresentChancePercent =
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

        private void FireHSpeedKforce(IHspeedKforce target)
        {
            if (duck != null)
                _kickForce = Math.Abs(duck.hSpeed) < 0.1f ? target.KickForceSlowAr : target.KickForceFastAr;
        }

        private void FireRandKforce(IRandKforce target)
        {
            _kickForce = Rando.Float(target.KickForce1Lmg, target.KickForce2Lmg);
        }

        private void FireFirstKforce(IFirstKforce target)
        {
            if (target.CurrentDelaySmg <= 0)
                _kickForce += target.KickForceDeltaSmg;
            target.CurrentDelaySmg = target.MaxDelaySmg;
        }

        private void FireKforce()
        {
            switch (this)
            {
                case IHspeedKforce thisAr:
                    FireHSpeedKforce(thisAr);
                    break;
                case IRandKforce thisLmg:
                    FireRandKforce(thisLmg);
                    break;
                case IFirstKforce thisSmg:
                    FireFirstKforce(thisSmg);
                    break;
            }
        }

        private void AddNyCase() => Level.Add(new NewYearCase(x, y));

        private void FireAccuracy()
        {
            switch (this)
            {
                case ILoseAccuracy thisDmr:
                    ammoType.accuracy = ClipAccuracy(ammoType.accuracy - thisDmr.DrainAccuracyDmr);
                    break;
                case IFirstPrecise thisFirstPrecise:
                    thisFirstPrecise.CurrentDelayFp = thisFirstPrecise.MaxDelayFp;
                    break;
            }
        }

        private void MaybeAddNyCase()
        {
            if (Rando.Float(0f, 1f) < PresentChancePercent / 100f) AddNyCase();
        }

        private void OnAmmoSpent()
        {
            FireAccuracy();
            MaybeAddNyCase();
        }

        private void FireWithKforce()
        {
            var previousAmmo = ammo;
            base.Fire();
            if (ammo < previousAmmo)
                OnAmmoSpent();
        }

        public override void Fire()
        {
            PrevKforce = _kickForce;
            FireKforce();
            FireWithKforce();
            if (ToPrevKforce)
                _kickForce = PrevKforce;
        }

        private float ClipAccuracy(float accuracy)
        {
            return Maths.Clamp(accuracy, MinAccuracy, BaseAccuracy);
        }

        public override void Update()
        {
            if (!_currHoneInit)
            {
                _currHoneInit = true;
                CurrHone = _holdOffset;
            }

            HoldOffsetNoExtra = CurrHone;
            CurrHone = HoldOffsetNoExtra;

            switch (this)
            {
                case IFirstKforce thisSmg:
                    thisSmg.CurrentDelaySmg -= 1;
                    if (thisSmg.CurrentDelaySmg < 0)
                        thisSmg.CurrentDelaySmg = 0;
                    break;
            }

            switch (this)
            {
                case ISpeedAccuracy thisSr:
                    ammoType.accuracy = duck != null
                        ? ClipAccuracy(
                            BaseAccuracy
                            +
                            thisSr.SpeedAccuracyThreshold
                            -
                            (
                                Math.Abs(duck.hSpeed) * thisSr.SpeedAccuracyHorizontal
                                +
                                Math.Abs(duck.vSpeed) * thisSr.SpeedAccuracyVertical
                            )
                        )
                        : BaseAccuracy;
                    break;
                case ILoseAccuracy thisDmr:
                    ammoType.accuracy = ClipAccuracy(ammoType.accuracy + thisDmr.RegenAccuracyDmr);
                    break;
                case IFirstPrecise thisFirstPrecise:
                    thisFirstPrecise.CurrentDelayFp = Math.Max(thisFirstPrecise.CurrentDelayFp - 1, 0);
                    ammoType.accuracy = thisFirstPrecise.CurrentDelayFp <= 0f
                        ? thisFirstPrecise.MaxAccuracyFp
                        : BaseAccuracy;
                    break;
            }

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

        protected bool BipodsQ()
        {
            return BipodsQ(this);
        }

        protected bool HandleQ()
        {
            return HandleQ(this);
        }

        protected bool SwitchStockQ()
        {
            return SwitchStockQ(this);
        }

        [PublicAPI]
        public static void SetSpriteMapFrameId(SpriteMap sm, int value, int m)
        {
            value = (value % m + m) % m;
            sm.frame = value;
        }

        public override void Draw()
        {
            base.Draw();
#if DEBUG
            if (Level.activeLevel is Editor) return;
            if (duck is null) return;
            {
                if (ammoType is null) return;
                var a = (1 - ammoType.accuracy) / 2;
                var v = OffsetLocal(new Vec2(64, 0));
                Graphics.DrawLine(barrelPosition, barrelPosition + v.Rotate(a, Vec2.Zero), Color.Red);
                Graphics.DrawLine(barrelPosition, barrelPosition + v.Rotate(-a, Vec2.Zero), Color.Red);
            }
            {
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
#endif
        }
    }
}
