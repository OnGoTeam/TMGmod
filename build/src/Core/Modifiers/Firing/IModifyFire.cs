using System;

namespace TMGmod.Core.Modifiers.Firing
{
    public interface IModifyFire
    {
        void ModifyFire(Action fire);
        bool CanFire();
    }
}
