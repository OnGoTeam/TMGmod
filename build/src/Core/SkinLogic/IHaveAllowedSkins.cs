using System.Collections.Generic;

namespace TMGmod.Core.SkinLogic
{
    public interface IHaveAllowedSkins : IHaveSkin
    {
        ICollection<int> AllowedSkins { get; }
    }
}
