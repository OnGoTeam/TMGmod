﻿using DuckGame;

namespace TMGmod.NY
{
    public class SnowBullet:Bullet
    {
        public SnowBullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _tracer = false;
        }
    }
}