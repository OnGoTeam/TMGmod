using System;
using DuckGame;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseGun:Gun
    {
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
                    thisSmg.Kdelay = 20f;
                    break;
                case IAmSr thisSr:
                    ammoType.accuracy = (duck != null ? Math.Max(Math.Min(1f, 1f - Math.Abs(duck.hSpeed) - Math.Abs(duck.vSpeed)), 0f) : 1f) * thisSr.BaseAccuracy;
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
            base.Update();
        }
    }
}