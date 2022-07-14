using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IDeployBipods : IHaveBipodState, IAmAGun
    {
        [UsedImplicitly] NetSoundEffect BipOff { get; }

        [UsedImplicitly] StateBinding BipOffBinding { get; }

        [UsedImplicitly] NetSoundEffect BipOn { get; }

        [UsedImplicitly] StateBinding BipOnBinding { get; }
    }
}
