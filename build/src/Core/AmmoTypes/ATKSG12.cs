using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATKSG12 : BaseAmmoType
    {
        public ATKSG12()
        {
            accuracy = 0.4f;
            penetration = 1f;
            range = 185f;
            deadly = true;
            weight = 5f;
            bulletSpeed = 25f;
            bulletThickness = 0.5f;
            immediatelyDeadly = true;
            BulletDamage = 16f;
            DeltaDamage = 0.75f;
            AlphaDamage = 0.4f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shell = new Gauge12Shell(x, y)
            {
                hSpeed = (1f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = 0.75f + Rando.Float(-0.5f, 0.5f),
                depth = -0.2f - Rando.Float(0.0f, 0.1f),
            };
            Level.Add(shell);
        }
    }
}
