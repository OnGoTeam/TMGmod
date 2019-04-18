using DuckGame;

namespace TMGmod.Core
{
    // ReSharper disable once InconsistentNaming
    public class ATNB : AmmoType
    {
        public ATNB()
        {
			bulletLength = 1f;
            bulletThickness = 0.1f;
            accuracy = 1f;
            combustable = true;
            bulletSpeed = 5f;
			range = 55f;
			penetration = 228f;
        }
    }
}