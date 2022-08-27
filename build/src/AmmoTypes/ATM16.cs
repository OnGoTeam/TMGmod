#if FEATURES_1_2_X
using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATM16 : BaseAmmoType
    {
        public ATM16()
        {
            bulletLength = 30f;
            bulletSpeed = 110f;
            range = 450f;
            accuracy = 0.91f;
            penetration = 2f;
            bulletThickness = 1.5f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var difficultToSee = new X3XShell(x, y)
            {
                hSpeed = Rando.Float(-.5f, .5f),
                FrameId = 0,
            }; // должна быть кастомная shell
            add(difficultToSee);
        }
    }
}
#endif
