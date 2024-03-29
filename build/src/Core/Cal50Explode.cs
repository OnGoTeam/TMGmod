﻿using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.Core
{
    public class Cal50Explode : BaseAmmoType, IHeavyAmmoType
    {
        public Cal50Explode()
        {
            accuracy = 1f;
            range = 1100f;
            penetration = 1f;
            bulletThickness = 2.5f;
            bulletSpeed = 55f;
            bulletType = typeof(ExplosiveBullet);
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var redpill = new M50Shell(x, y)
            {
                hSpeed = dir * Rando.Float(-.5f, .5f),
                vSpeed = -3f,
            };
            add(redpill);
        }
    }
}
