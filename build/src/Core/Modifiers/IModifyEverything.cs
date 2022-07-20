using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.Modifiers.Updating;

namespace TMGmod.Core.Modifiers
{
    public interface IModifyEverything : IModifySpent, IModifyUpdate, IModifyAccuracy, IModifyKforce, ISync
    {
    }
}
