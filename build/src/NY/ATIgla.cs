using DuckGame;
using TMGmod.Core;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATIglu : AmmoType
    {
        public readonly SpriteMap SpriteY;
        public ATIglu()
        {
            SpriteY = new SpriteMap(new Nothing().GetPath("Holiday/Igolka"), 4, 1);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
        }
    }
}
