using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IHaveSkin
    {
        [UsedImplicitly]
        int FrameId { set; }
        [UsedImplicitly]
        StateBinding FrameIdBinding { get; }
        [UsedImplicitly]
        EditorProperty<int> Skin { get; }
    }
}