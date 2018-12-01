using DuckGame;

namespace TMGmod.src
{
    public class HA : AmmoType
    {
        public HA()
        {
            this.accuracy = 0.9f;
            this.range = 700f;
            this.penetration = 4f;
            this.combustable = true;
            this.bulletSpeed = 40f;
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
