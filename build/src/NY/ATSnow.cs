using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    /// <inheritdoc />
    public class ATSneg : BaseAmmoTypeT
    {
        /// <summary>
        /// bullet sprite
        /// </summary>
        public readonly SpriteMap SpriteY;

        /// <inheritdoc />
        public ATSneg()
        {
            bulletType = typeof(SnowBullet);
            SpriteY = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/snow"), 6, 5);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
            bulletSpeed = 5f;
            range = 500f;
            accuracy = 0.95f;
            bulletLength = 3f;
            affectedByGravity = true;
            barrelAngleDegrees = -13.5f;
        }
    }
}
