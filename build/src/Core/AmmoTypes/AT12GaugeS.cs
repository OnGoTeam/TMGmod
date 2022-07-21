using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT12GaugeS : AT12Gauge
    {
        public AT12GaugeS() //KS-23' selfAT
        {
            range = 169f;
            accuracy = 0.33f;
            bulletSpeed = 50f;
            bulletThickness = 0f;
            bulletLength = 0f;
            DamageMean = 22f;
            AlphaDamage = 0.67f;
        }
    }
}
