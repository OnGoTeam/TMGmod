using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    /// <inheritdoc />
    public class ATCane : BaseAmmoTypeT
    {
        /// <summary>
        /// bullet sprite
        /// </summary>
        public readonly SpriteMap SpriteY;

        /// <inheritdoc />
        public ATCane()
        {
            bulletType = typeof(CandyCaneBullet);
            SpriteY = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/candycane"), 18, 7);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
            bulletLength = 3f;
            bulletSpeed = 15f;
        }
    }
}
