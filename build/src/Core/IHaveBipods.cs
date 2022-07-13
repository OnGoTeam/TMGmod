using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IHaveBipods
    {
        bool Bipods { get; set; }

        [UsedImplicitly] BitBuffer BipodsBuffer { get; set; }

        [UsedImplicitly] StateBinding BipodsBinding { get; }

        bool BipodsDisabled { get; }
    }

    public interface ICanDisableBipods: IHaveBipods, IAmAGun
    {
        void SetBipodsDisabled(bool disabled);
    }

    public interface ISwitchBipods : IHaveBipods
    {
        bool SwitchingBipods();
    }
}
