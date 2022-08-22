#if FEATURES_1_2
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
            combustable = true;
            bulletSpeed = 110f;
            range = 450f;
            accuracy = 0.91f;
            penetration = 2f;
            bulletThickness = 1.5f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var difficultToSee = new X3XShell(x, y, 0)
                { hSpeed = dir * (5f + Rando.Float(1f)) }; //������ ���� ��������� shell
            add(difficultToSee);
        }
    }
}
#endif