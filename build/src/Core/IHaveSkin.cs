using JetBrains.Annotations;

namespace TMGmod.Core
{
    [PublicAPI]
    public interface IHaveSkin
    {
        int FrameId { set; }
    }
}