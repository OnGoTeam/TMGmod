namespace TMGmod.Core.BipodsLogic
{
    public interface IDeployBipods : IHaveBipodState, IAmAGun
    {
        string BipOff { get; }

        string BipOn { get; }
    }
}
