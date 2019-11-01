using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IHaveBipods
    {
        [UsedImplicitly]
        bool Bipods { get; set; }
        [UsedImplicitly]
        StateBinding BipodsBinding { get; }
        bool BipodsDisabled { get; }
    }
}
