using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATSneg : BaseAmmoTypeT
    {
        public ATSneg()
        {
            bulletType = typeof(SnowBullet);
            var spriteY = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/snow"), 6, 5);
            spriteY.CenterOrigin();
            sprite = spriteY;
            bulletSpeed = 5f;
            range = 500f;
            accuracy = 0.95f;
            bulletLength = 3f;
            affectedByGravity = true;
            barrelAngleDegrees = -13.5f;
        }
    }
}
