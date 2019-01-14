using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.NY;

namespace TMGmod.Core.WClasses
{
    [PublicAPI]
    public abstract class BaseGun:Gun
    {
        protected float BaseAccuracy = 1f;
        protected float MinAccuracy;
        protected float PrevKforce;
        protected bool ToPrevKforce;
        protected Vec2 ShellOffset;
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
        
        public override void Update()
        {
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

        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell)
                {
                    _ammoType.PopShell(x + ShellOffset.x, y + ShellOffset.y, -offDir);
                }
                --ammo;
            }
            loaded = true;
        }
    }
}