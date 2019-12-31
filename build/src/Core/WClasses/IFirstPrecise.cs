namespace TMGmod.Core.WClasses
{
    public interface IFirstPrecise
    {
        int CurrDelay { get; set; }
        int MaxDelayFp { get; }
        float MaxAccuracy { get; }
    }
}