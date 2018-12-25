using DuckGame;

namespace TMGmod.Core
{
    public interface IHaveSkin
    {
        int FrameId { set; }
        StateBinding FrameIdBinding { get; }
    }
}