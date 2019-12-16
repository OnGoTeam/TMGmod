using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// <see cref="AmmoType"/> for <see cref="X3X"/>
    /// </summary>
    public class ATx3x : AmmoType
    {
        /// <inheritdoc />
        public ATx3x()
        {
			bulletLength = 30f;
            combustable = true;
            bulletSpeed = 15f;
            range = 800f;
            accuracy = 1f;
            penetration = 100f;
            bulletThickness = 5f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var flyingtoilet = new X3XShell(x, y) {hSpeed = dir * (7f + Rando.Float(1f))};
            Level.Add(flyingtoilet);
        }
    }
}
