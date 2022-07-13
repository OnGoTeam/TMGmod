/*using System;
using DuckGame;

namespace TMGmod.Core
{
    public static class ExploCreator
    {
        public static void CreateExplosion(Vec2 pos)
        {
            var cx = pos.x;
            var cy = pos.y - 2f;
            Level.Add(new ExplosionPart(cx, cy));
            var num = 6;
            if (Graphics.effectsLevel < 2) num = 3;
            for (var i = 0; i < num; i++)
            {
                var dir = i * 60f + Rando.Float(-10f, 10f);
                var dist = Rando.Float(12f, 20f);
                var ins = new ExplosionPart(cx + (float) (Math.Cos(Maths.DegToRad(dir)) * dist),
                    cy - (float) (Math.Sin(Maths.DegToRad(dir)) * dist));
                Level.Add(ins);
            }

            SFX.Play("explode");
        }
    }
}*/


