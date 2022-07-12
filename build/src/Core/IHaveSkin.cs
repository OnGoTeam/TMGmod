using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IHaveSkin : IHaveFrameId
    {
        [UsedImplicitly]
        EditorProperty<int> Skin { get; }
    }

    public interface IHaveFrameId
    {
        [UsedImplicitly]
        int FrameId { set; }
        [UsedImplicitly]
        StateBinding FrameIdBinding { get; }
    }
}