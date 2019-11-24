using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    /// <inheritdoc />
    public class ATIglu : BaseAmmoTypeT
    {
        /// <inheritdoc />
        public ATIglu()
        {
            var spriteY = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/Igolka"), 4, 1);
            spriteY.CenterOrigin();
            sprite = spriteY;
            penetration = 1f;
            bulletLength = 0f;
            bulletThickness = 0.25f;
        }
    }
}
