using DuckGame;
using TMGmod.Core;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATSneg : AmmoType
    {
        public readonly SpriteMap SpriteY;
        public ATSneg()
        {
            bulletType = typeof(SnowBullet);
            SpriteY = new SpriteMap(new Nothing().GetPath("Holiday/snow"), 6, 5);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
        }
    }
}
