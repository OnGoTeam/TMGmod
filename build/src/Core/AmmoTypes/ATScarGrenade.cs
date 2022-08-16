using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATScarGrenade: ATGrenade
    {
        public ATScarGrenade()
        {
            range = 2500f;
            accuracy = 1f;
            bulletSpeed = 18f;
            barrelAngleDegrees = -7.5f;
        }

        public override void PopShell(float x, float y, int dir)
        {
        }
    }
}
