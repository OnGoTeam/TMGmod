namespace TMGmod.Core.WClasses
{
    public interface IFirstKforce
    {
        float KickForceDeltaSmg { get; }
        uint CurrentDelaySmg { get; set; }
        uint MaxDelaySmg { get; }
    }
}
