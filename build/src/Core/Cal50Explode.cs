using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{

    // ReSharper disable once InconsistentNaming
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
