using DuckGame;

namespace TMGmod.src
{
    public class ATNB : AmmoType
    {
        public ATNB()
        {
			this.bulletLength = 1f;
			this.accuracy = 1f;
            this.combustable = true;
            this.bulletSpeed = 5f;
			this.range = 0.3f;
			this.penetration = 175f;
        }
    }
}