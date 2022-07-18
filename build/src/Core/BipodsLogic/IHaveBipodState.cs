using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.BipodsLogic
{
    public interface IHaveBipodState : IHaveBipods
    {
        float BipodsState { get; set; }
        [UsedImplicitly] StateBinding BsBinding { get; }
        float BipodSpeed { get; }
        void UpdateBipodsStats(float old);
    }
}
