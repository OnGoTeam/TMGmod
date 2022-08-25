using System;
using DuckGame;

namespace TMGmod.Core.BipodsLogic
{
    public static class BipodsImplementation
    {
        public static bool BipodsQ(this Gun gun)
        {
            var duck = gun.duck;
            return !(duck is null) && !gun.raised && (duck.crouch || duck.sliding) && duck.grounded &&
                   Math.Abs(duck.hSpeed) < 0.05f;
        }
    }
}
