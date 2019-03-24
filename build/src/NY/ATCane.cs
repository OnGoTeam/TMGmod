using DuckGame;
using ogtdglib;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATCane : BaseAmmoTypeT
    {
        public readonly SpriteMap SpriteY;
        public ATCane()
        {
            bulletType = typeof(CandyCaneBullet);
            SpriteY = new SpriteMap(new Nothing(Core.TMGmod.LastInstance).GetPath("Holiday/candycane"), 18, 7);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
            bulletLength = 3f;
            bulletSpeed = 15f;
        }
    }
}
