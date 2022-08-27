using System;
using DuckGame;

namespace TMGmod.Core.BipodsLogic
{
    public static class BipodsImplementation
    {
        public static bool BipodsQ(this Gun gun)
        {
            var duck = gun.duck;
            return duck is { grounded: true } &&
                   (duck.crouch || duck.sliding) &&
                   Math.Abs(duck.hSpeed) < 0.05f &&
                   !gun.raised;
        }
    }
}
