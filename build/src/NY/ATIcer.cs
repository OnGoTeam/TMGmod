using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    /// <inheritdoc />
    public class ATIcer : BaseAmmoTypeT
    {
        /// <inheritdoc />
        public ATIcer()
        {
            var spriteY = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/Icer Bullet"), 16, 5);
            spriteY.CenterOrigin();
            sprite = spriteY;
            penetration = 0f;
            accuracy = 1f;
            bulletSpeed = 10f;
            range = 1000f;
            bulletLength = 5f;
            bulletThickness = 3f;
        }
    }
}
