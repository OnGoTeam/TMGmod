using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IHaveBipodState : IHaveBipods
    {
        float BipodsState { get; set; }
        [UsedImplicitly] StateBinding BsBinding { get; }
        void UpdateStats(float old);
        float BipodSpeed { get; }
    }
}
