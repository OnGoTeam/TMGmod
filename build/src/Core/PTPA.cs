using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGMod.src
{
    public class PTPA : AmmoType
    {
        public PTPA()
        {
            this.accuracy = 0.9f;
            this.range = 500f;
            this.penetration = 5f;
            this.combustable = true;
            this.bulletSpeed = 30f;
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
