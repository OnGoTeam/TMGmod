using DuckGame;

namespace TMGmod.Core
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
            range = 500f;
            accuracy = 1f;
            penetration = 100f;
            bulletThickness = 5f;
        }
    }
}
