namespace TMGmod.Core
{
    public interface IDeployBipods : IHaveBipodState, IAmAGun
    {
        string BipOff { get; }

        string BipOn { get; }
    }
}
