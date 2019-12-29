using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    /// <inheritdoc />
    public class ATCane : BaseAmmoTypeT
    {
        /// <inheritdoc />
        public ATCane()
        {
            bulletType = typeof(CandyCaneBullet);
            var spriteY = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/candycane"), 18, 7);
            spriteY.CenterOrigin();
            sprite = spriteY;
            bulletLength = 3f;
            bulletSpeed = 15f;
        }
    }
}