using System;
using DuckGame;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATPMR30SH: AT12Gauge
    {
        public ATPMR30SH()
        {
            range = 110f;
            accuracy = 0.35f;
            penetration = 2f;
            bulletSpeed = 50f;
            DamageMean = 15f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
        }
    }
}
