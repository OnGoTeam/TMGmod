using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.SkinLogic
{
    public interface IHaveFrameId
    {
        int FrameId { get; set; }

        [UsedImplicitly] StateBinding FrameIdBinding { get; }
    }
}
