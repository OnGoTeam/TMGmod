using DuckGame;
using JetBrains.Annotations;
using System;
using TMGmod.NY;

namespace TMGmod.Core.WClasses
{
    [UsedImplicitly]
    public abstract class BaseGun : Gun
    {
        protected float BaseAccuracy = 1f;
        protected float MinAccuracy;
        [UsedImplicitly]
        protected float PrevKforce;
        private const float PresentChancePercent = 0.5f; //значение указано в процентах. Вне праздников - 0,1%, во время праздников - 2%, до 1.2 оставить 0,5%
        [UsedImplicitly]
        protected bool ToPrevKforce;
        protected Vec2 ShellOffset;
        protected Vec2 CurrHone;
        private bool _currHoneInit;
        [UsedImplicitly]
        protected Vec2 ExtraHoldOffset => duck == null ? new Vec2(0, 0) : !duck.sliding ? new Vec2(0, 0) : new Vec2(0, 1);
        [UsedImplicitly]
        protected Vec2 HoldOffsetNoExtra
        {
            get => _holdOffset - ExtraHoldOffset;
            set => _holdOffset = value + ExtraHoldOffset;
        }
        protected BaseGun(float xval, float yval) : base(xval, yval)
        {
            ToPrevKforce = true;
        }

        public override void Fire()
        {
            PrevKforce = _kickForce;
            switch (this)
            {
                case IHspeedKforce thisAr:
                    if (duck != null)
                        _kickForce = Math.Abs(duck.hSpeed) < 0.1f ? thisAr.KickForceSlowAr : thisAr.KickForceFastAr;
                    break;
                case IRandKforce thisLmg:
                    _kickForce = Rando.Float(thisLmg.KickForce1Lmg, thisLmg.KickForce2Lmg);
                    break;
                case IFirstKforce thisSmg:
                    if (thisSmg.CurrentDelaySmg <= 0)
                        _kickForce += thisSmg.KickForceDeltaSmg;
                    thisSmg.CurrentDelaySmg = thisSmg.MaxDelaySmg;
                    break;
            }

            var pammo = ammo;
            base.Fire();
            if (pammo > ammo)
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

                if (Rando.Float(0f, 1f) < PresentChancePercent / 100f)
                {
                    var scase = new NewYearCase(x, y);
                    Level.Add(scase);
                }
            }
            if (ToPrevKforce)
                _kickForce = PrevKforce;
        }

        private float ClipAccuracy(float accuracy)
        {
            return Math.Min(BaseAccuracy, Math.Max(MinAccuracy, accuracy));
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
                    ammoType.accuracy = duck != null ? ClipAccuracy(BaseAccuracy + thisSr.MuAccuracySr - (Math.Abs(duck.hSpeed) + Math.Abs(duck.vSpeed) * thisSr.LambdaAccuracySr)) : BaseAccuracy;
                    break;
                case ILoseAccuracy thisDmr:
                    ammoType.accuracy = ClipAccuracy(ammoType.accuracy + thisDmr.RegenAccuracyDmr);
                    break;
                case IFirstPrecise thisFirstPrecise:
                    thisFirstPrecise.CurrentDelayFp = Math.Max(thisFirstPrecise.CurrentDelayFp - 1, 0);
                    ammoType.accuracy = thisFirstPrecise.CurrentDelayFp <= 0f ? thisFirstPrecise.MaxAccuracyFp : BaseAccuracy;
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
            return !(duck is null) && !gun.raised && (duck.crouch || duck.sliding) && duck.grounded && Math.Abs(duck.hSpeed) < 0.05f;
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