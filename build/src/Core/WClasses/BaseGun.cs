using System;
using DuckGame;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseGun:Gun
    {
        protected float BaseAccuracy;
        protected BaseGun(float xval, float yval) : base(xval, yval)
        {
        }

        public override void Fire()
        {
            var prevKforce = _kickForce;
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
                case IAmSr thisSr:
                    ammoType.accuracy = (duck != null ? Math.Max(Math.Min(1f, 1f - Math.Abs(duck.hSpeed) - Math.Abs(duck.vSpeed)), 0f) * (1f - thisSr.MinAccuracy) + thisSr.MinAccuracy : 1f) * BaseAccuracy;
                    break;
                case IFirstPrecise thisFirstPrecise:
                    ammoType.accuracy = thisFirstPrecise.CurrDelay <= 0f ? thisFirstPrecise.MaxAccuracy : BaseAccuracy;
                    thisFirstPrecise.CurrDelay = thisFirstPrecise.MaxDelayFp;
                    break;
            }

            base.Fire();
            _kickForce = prevKforce;
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