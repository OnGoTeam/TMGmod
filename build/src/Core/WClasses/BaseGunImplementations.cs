using System;
using DuckGame;

namespace TMGmod.Core.WClasses
{
    public static class BaseGunImplementations
    {
        public static bool HandleQ(this Gun gun)
        {
            var duck = gun.duck;
            return !(duck is null) && !gun.raised && duck.sliding && duck.grounded && Math.Abs(duck.hSpeed) < 1f;
        }
    }
}
