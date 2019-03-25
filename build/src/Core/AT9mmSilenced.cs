using DuckGame;

namespace TMGmod.Core
{
    // ReSharper disable once InconsistentNaming
    public class AT9mmS : AmmoType
    {
        public AT9mmS()
        {
			bulletLength = 0f;
            combustable = true;
            bulletSpeed = 37f;
            penetration = 0.5f;
        }
    }
}
