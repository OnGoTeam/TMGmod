using System.Linq;
using DuckGame;

namespace TMGmod.Core.SkinLogic
{
    public static class SkinImplementation
    {
        private static int FilterSkin(this IHaveAllowedSkins target, int skin)
        {
            return target.AllowedSkins.Contains(skin)
                ? skin
                : Rando.ChooseInt(target.AllowedSkins.ToArray());
        }

        public static void UpdateSkin(this IHaveAllowedSkins target)
        {
            target.Skin = target.FilterSkin(target.SkinValue);
        }
    }
}
