namespace TMGmod.Core
{
    public interface ICanDisableBipods : IHaveBipods, IAmAGun
    {
        void SetBipodsDisabled(bool disabled);
    }
}
