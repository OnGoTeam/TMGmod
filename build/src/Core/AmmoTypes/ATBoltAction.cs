using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATBoltAction : BaseAmmoType
    {
        public ATBoltAction()
        {
            accuracy = 1f;
            penetration = 1f;
            bulletSpeed = 75f;
            range = 1000f;
            deadly = true;
            bulletThickness = 0f;
            bulletLength = 40f;
            immediatelyDeadly = true;
            BulletDamage = 110f;
            DeltaDamage = 0.05f;
            AlphaDamage = 1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT762NATOShell(x, y) //spinershell
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}
