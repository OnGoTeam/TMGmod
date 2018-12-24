using System;
using DuckGame;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseGun:Gun
    {
        protected float BaseAccuracy = 1f;
        protected float MinAccuracy;
        protected float PrevKforce;
        protected readonly bool ToPrevKforce = true;
        protected BaseGun(float xval, float yval) : base(xval, yval)
        {
        }

        public override void Fire()
        {
            PrevKforce = _kickForce;
            switch (this)
            {
                case IAmAr thisAr:
                    if (duck != null)
                        _kickForce = Math.Abs(duck.hSpeed) < 0.1f ? thisAr.Kforce1Ar : thisAr.Kforce2Ar;
                    break;
                case IAmLmg thisLmg:
                    _kickForce = Rando.Float(thisLmg.Kforce1Lmg, thisLmg.Kforce2Lmg);
                    break;
                case IAmSmg thisSmg:
                    if (thisSmg.CurrDelaySmg <= 0)
                        _kickForce += thisSmg.KforceDSmg;
                    thisSmg.CurrDelaySmg = thisSmg.MaxDelaySmg;
                    break;
            }

            switch (this)
            {
                case IAmSr _:
                    ammoType.accuracy = duck != null ? Math.Min(Math.Max(MinAccuracy, BaseAccuracy + 1f - duck.hSpeed / 2 - duck.vSpeed / 2), BaseAccuracy): BaseAccuracy;
                    break;
                case IFirstPrecise thisFirstPrecise:
                    ammoType.accuracy = thisFirstPrecise.CurrDelay <= 0f ? thisFirstPrecise.MaxAccuracy : BaseAccuracy;
                    thisFirstPrecise.CurrDelay = thisFirstPrecise.MaxDelayFp;
                    break;
            }

            base.Fire();
            if (ToPrevKforce)
                _kickForce = PrevKforce;
        }
        
        public override void Update()
        {
            switch (this)
            {
                case IAmSmg thisSmg:
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
    }
}