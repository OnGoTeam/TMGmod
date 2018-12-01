using DuckGame;

namespace TMGmod.src
{
    public class HA : AmmoType
    {
        public HA()
        {
            accuracy = 0.9f;
            range = 700f;
            penetration = 4f;
            combustable = true;
            bulletSpeed = 40f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            Level.Add(new PistolShell(x, y)
            {
                hSpeed = (float)dir * (1.5f + Rando.Float(1f))
            });
        }
    }
}
