namespace TMGmod.Core.WClasses
{
    public interface IFirstKforce
    {
        float KickForceDeltaSmg { get; }
        int CurrentDelaySmg { get; set; }
        int MaxDelaySmg { get; }
    }
}