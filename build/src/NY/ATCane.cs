using DuckGame;
using TMGmod.Core;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATCane : AmmoType
    {
        public readonly SpriteMap SpriteY;
        public ATCane()
        {
            bulletType = typeof(CandyCaneBullet);
            SpriteY = new SpriteMap(new Nothing().GetPath("Holiday/candycane"), 18, 7);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
        }
    }
}
