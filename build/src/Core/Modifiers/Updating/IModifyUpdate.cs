using System;

namespace TMGmod.Core.Modifiers.Updating
{
    public interface IModifyUpdate
    {
        void ModifyUpdate(Action update);
    }
}
