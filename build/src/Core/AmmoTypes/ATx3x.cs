using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATx3x : BaseAmmoType
    {
        public ATx3x()
        {
			bulletLength = 45f;
            combustable = true;
            bulletSpeed = 15f;
            range = 800f;
            accuracy = 1f;
            penetration = 100f;
            bulletThickness = 4f;
            BulletDamage = 365f;
            DeltaDamage = 0f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var flyingtoilet = new X3XShell(x, y) {hSpeed = dir * (7f + Rando.Float(1f))};
            Level.Add(flyingtoilet);
        }
    }
}
