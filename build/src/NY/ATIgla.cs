using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATIglu : BaseAmmoTypeT
    {
        public readonly SpriteMap SpriteY;
        public ATIglu()
        {
            SpriteY = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/Igolka"), 4, 1);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
            penetration = 1f;
            bulletLength = 0f;
            bulletThickness = 0.25f;
        }
    }
}
