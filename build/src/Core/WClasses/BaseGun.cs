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
                        _kickForce = Math.Abs(duck.hSpeed) < 0.1f ? thisAr.Kforce1 : thisAr.Kforce2;
                    break;
                case IAmLmg thisLmg:
                    _kickForce = Rando.Float(thisLmg.KforceR1, thisLmg.KforceR2);
                    break;
                case IAmSmg thisSmg:
                    if (thisSmg.Kdelay <= 0f)
                        _kickForce += thisSmg.KforceD;
                    thisSmg.Kdelay = 5f;
                    break;
            }

            switch (this)
            {
                case IAmSr _:
                    ammoType.accuracy = (duck != null ? Math.Max(Math.Min(1f, 1f - Math.Abs(duck.hSpeed) - Math.Abs(duck.vSpeed)), 0f) : 1f) * BaseAccuracy;
                    break;
                case IFirstPrecise thisFirstPrecise:
                    ammoType.accuracy = thisFirstPrecise.CurrDelay <= 0f ? thisFirstPrecise.MaxAccuracy : BaseAccuracy;
                    thisFirstPrecise.CurrDelay = thisFirstPrecise.MaxDelay;
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
                    thisSmg.Kdelay -= 0.1f;
                    if (thisSmg.Kdelay < 0f)
                        thisSmg.Kdelay = 0f;
                    break;
            }

            switch (this)
            {
                case IFirstPrecise thisFirstPrecise:
                    thisFirstPrecise.CurrDelay -= Math.Max(thisFirstPrecise.CurrDelay - 0.1f, 0f);
                    break;
            }
            base.Update();
        }
    }
}