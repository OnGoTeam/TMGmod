using DuckGame;
using ogtdglib;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATIglu : BaseAmmoTypeT
    {
        public readonly SpriteMap SpriteY;
        public ATIglu()
        {
            SpriteY = new SpriteMap(new Nothing(Core.TMGmod.LastInstance).GetPath("Holiday/Igolka"), 4, 1);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
            penetration = 1f;
            bulletLength = 0f;
            bulletThickness = 0.25f;
        }
    }
}
