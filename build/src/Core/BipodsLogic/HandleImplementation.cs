using System;
using DuckGame;

namespace TMGmod.Core.BipodsLogic
{
    public static class HandleImplementation
    {
        public static bool HandleQ(this Gun gun)
        {
            var duck = gun.duck;
            return duck is not null && duck.sliding && duck.grounded && Math.Abs(duck.hSpeed) < 1f && !gun.raised;
        }
    }
}
