using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.SkinLogic
{
    public interface IHaveFrameId
    {
        int FrameId { set; }

        [UsedImplicitly] StateBinding FrameIdBinding { get; }
    }
}
