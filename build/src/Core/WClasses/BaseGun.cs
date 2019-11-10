using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.NY;

namespace TMGmod.Core.WClasses
{
    /// <summary>
    /// BaseGun implements some useful features
    /// Used to not to write smth again and over
    ///     implements wclasses specs
    ///         <see cref="IFirstKforce"/>
    ///         <see cref="IFirstPrecise"/>
    ///         <see cref="IHspeedKforce"/>
    ///         <see cref="IRandKforce"/>
    ///         <see cref="ISpeedAccuracy"/>
    ///     dynamic holdOffset
    ///     random cases (<see cref="NewYearCase"/>) dropped from firing
    /// </summary>
    [PublicAPI]
    public abstract class BaseGun:Gun
    {
        /// <summary>
        /// Base accuracy used by <see cref="ISpeedAccuracy"/> and <see cref="IFirstPrecise"/>
        /// </summary>
        protected float BaseAccuracy = 1f;
        /// <summary>
        /// Base accuracy used by <see cref="ISpeedAccuracy"/>
        /// </summary>
        protected float MinAccuracy;
        /// <summary>
        /// Used for custom Kforce modifying
        /// </summary>
        protected float PrevKforce;
        /// <summary>
        /// whether reset Kforce to <see cref="PrevKforce"/>
        /// </summary>
        protected bool ToPrevKforce;
        /// <summary>
        /// custom shell-drop offset (<seealso cref="Reload"/>)
        /// </summary>
        protected Vec2 ShellOffset;
        /// <summary>
        /// Current hold offset no extra
        /// </summary>
        protected Vec2 CurrHone;
        private bool _currHoneInit;

        /// <summary>
        /// Extra holdOffset for sliding
        /// </summary>
        protected Vec2 ExtraHoldOffset => duck == null ? new Vec2(0, 0) : !duck.sliding ? new Vec2(0, 0) : new Vec2(0, 1);

        /// <summary>
        /// holdOffset - <see cref="ExtraHoldOffset"/>
        /// </summary>
        protected Vec2 HoldOffsetNoExtra
        {
            get => _holdOffset - ExtraHoldOffset;
            set => _holdOffset = value + ExtraHoldOffset;
        }

        /// <summary>
        /// stub for TPKF
        /// </summary>
        /// <param name="xval"></param>
        /// <param name="yval"></param>
        protected BaseGun(float xval, float yval) : base(xval, yval)
        {
            ToPrevKforce = true;
        }

        /// <summary>
        /// <see cref="Gun.Fire"/> modification/reimplementation
        /// </summary>
        public override void Fire()
        {
            PrevKforce = _kickForce;
            switch (this)
            {
                case IHspeedKforce thisAr:
                    if (duck != null)
                        _kickForce = Math.Abs(duck.hSpeed) < 0.1f ? thisAr.Kforce1Ar : thisAr.Kforce2Ar;
                    break;
                case IRandKforce thisLmg:
                    _kickForce = Rando.Float(thisLmg.Kforce1Lmg, thisLmg.Kforce2Lmg);
                    break;
                case IFirstKforce thisSmg:
                    if (thisSmg.CurrDelaySmg <= 0)
                        _kickForce += thisSmg.KforceDSmg;
                    thisSmg.CurrDelaySmg = thisSmg.MaxDelaySmg;
                    break;
            }

            switch (this)
            {
                case ISpeedAccuracy thisSr:
                    ammoType.accuracy = duck != null ? Math.Min(Math.Max(MinAccuracy, BaseAccuracy + thisSr.MuAccuracySr - (Math.Abs(duck.hSpeed) + Math.Abs(duck.vSpeed) * thisSr.LambdaAccuracySr)), BaseAccuracy): BaseAccuracy;
                    break;
                case IFirstPrecise thisFirstPrecise:
                    ammoType.accuracy = thisFirstPrecise.CurrDelay <= 0f ? thisFirstPrecise.MaxAccuracy : BaseAccuracy;
                    thisFirstPrecise.CurrDelay = thisFirstPrecise.MaxDelayFp;
                    break;
            }

            var pammo = ammo;
            base.Fire();
            if (pammo > ammo)
            {
                if (Rando.Float(0f, 1f) < 0.001f)
                {
                    var scase = new NewYearCase(x, y);
                    Level.Add(scase);
                }
            }
            if (ToPrevKforce)
                _kickForce = PrevKforce;
        }

        /// <summary>
        /// <see cref="Gun.Update"/> modification/reimplementation
        /// </summary>
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
                    thisSmg.CurrDelaySmg -= 1;
                    if (thisSmg.CurrDelaySmg < 0)
                        thisSmg.CurrDelaySmg = 0;
                    break;
            }

            switch (this)
            {
                case IFirstPrecise thisFirstPrecise:
                    thisFirstPrecise.CurrDelay = Math.Max(thisFirstPrecise.CurrDelay - 1, 0);
                    break;
            }
            base.Update();
        }

        /// <summary>
        /// <see cref="ShellOffset"/> realization
        /// </summary>
        /// <param name="shell"></param>
        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell)
                {
                    _ammoType.PopShell(Offset(ShellOffset).x, Offset(ShellOffset).y, -offDir);
                }
                --ammo;
            }
            loaded = true;
        }

        public static bool BipodsQ(Gun gun, bool bypassihb=false)
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

        public bool BipodsQ()
        {
            return BipodsQ(this);
        }
        public bool HandleQ()
        {
            return HandleQ(this);
        }
    }
}