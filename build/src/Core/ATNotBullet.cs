using DuckGame;

namespace TMGmod.Core
{
    // ReSharper disable once InconsistentNaming
    public class ATNB : AmmoType
    {
        public ATNB()
        {
			bulletLength = 1f;
			accuracy = 1f;
            combustable = true;
            bulletSpeed = 5f;
			range = 0.3f;
			penetration = 175f;
        }
    }
}