using DuckGame;

namespace TMGmod.src
{

    public class cal50explode : AmmoType
    {
        public cal50explode()
        {
            accuracy = 0.9f;
            range = 700f;
            penetration = 0f;
            combustable = true;
            bulletSpeed = 55f;
            bulletType = typeof(explode);
        }
    }
}
