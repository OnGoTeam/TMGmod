using System;
using DuckGame;

namespace TMGmod.Core.BipodsLogic
{
    public static class HandleImplementation
    {
        public static bool HandleQ(this Gun gun)
        {
            var duck = gun.duck;
            return duck is { sliding: true, grounded: true } && Math.Abs(duck.hSpeed) < 1f && !gun.raised;
        }
    }
}
