namespace TMGmod.Core.WClasses
{
    public interface IFirstPrecise
    {
        int CurrentDelayFp { get; set; }
        int MaxDelayFp { get; }
        float MaxAccuracyFp { get; }
    }
}
