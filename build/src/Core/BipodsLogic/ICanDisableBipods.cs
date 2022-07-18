namespace TMGmod.Core.BipodsLogic
{
    public interface ICanDisableBipods : IHaveBipods, IAmAGun
    {
        void SetBipodsDisabled(bool disabled);
    }
}
