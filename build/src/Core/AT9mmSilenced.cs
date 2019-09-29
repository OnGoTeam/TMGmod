using DuckGame;

namespace TMGmod.Core
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// <see cref="AmmoType"/> with invisible bullets for silenced weapons
    /// </summary>
    public class AT9mmS : AmmoType
    {
        /// <summary>
        /// sets bulletLength to 0f for invisibility
        /// </summary>
        public AT9mmS()
        {
            bulletLength = 0f;
            combustable = true;
            bulletSpeed = 37f;
            penetration = 0.4f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var pistolShell = new PistolShell(x, y) {hSpeed = dir * (1.5f + Rando.Float(1f))};
            Level.Add(pistolShell);
        }
    }
}
