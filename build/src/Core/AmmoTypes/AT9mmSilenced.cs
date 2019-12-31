using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT9mmS : AmmoType
    {
        public AT9mmS()
        {
            bulletLength = 0f;
            combustable = true;
            bulletSpeed = 37f;
            penetration = 0.4f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var pistolShell = new PistolShell(x, y) {hSpeed = dir * (1.5f + Rando.Float(1f))};
            Level.Add(pistolShell);
        }
    }
}
