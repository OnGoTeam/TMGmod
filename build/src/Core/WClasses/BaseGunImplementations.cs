using System;
using System.Linq;
using DuckGame;
using TMGmod.Core.SkinLogic;

namespace TMGmod.Core.WClasses
{
    public static class BaseGunImplementations
    {
        private static int FilterSkin(this IHaveAllowedSkins target, int skin) =>
            target.AllowedSkins.Contains(skin)
                ? skin
                : Rando.ChooseInt(target.AllowedSkins.ToArray());

        public static void UpdateSkin(this IHaveAllowedSkins target) =>
            target.FrameId = target.FilterSkin(target.Skin.value);

        public static bool HandleQ(this Gun gun)
        {
            var duck = gun.duck;
            return !(duck is null) && !gun.raised && duck.sliding && duck.grounded && Math.Abs(duck.hSpeed) < 1f;
        }
    }
}
