using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// <see cref="AmmoType"/> for not AT cases
    /// </summary>
    public class ATNB : AmmoType
    {
        /// <inheritdoc />
        public ATNB()
        {
			bulletLength = 1f;
            bulletThickness = 0.1f;
            accuracy = 1f;
            combustable = true;
            bulletSpeed = 5f;
			range = 50f;
			penetration = 228f;
        }
    }
}