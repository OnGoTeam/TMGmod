using DuckGame;

namespace TMGmod.src
{
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