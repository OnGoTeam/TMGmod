using DuckGame;

namespace TMGmod.src
{

    public class cal50explode : AmmoType
    {
        public cal50explode()
        {
            this.accuracy = 0.9f;
            this.range = 700f;
            this.penetration = 0f;
            this.combustable = true;
            this.bulletSpeed = 55f;
            this.bulletType = typeof(explode);
        }
    }
}
