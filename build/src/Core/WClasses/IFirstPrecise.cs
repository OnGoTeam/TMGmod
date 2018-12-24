namespace TMGmod.Core.WClasses
{
    public interface IFirstPrecise
    {
        float CurrDelay { get; set; }
        float MaxDelay { get; }
        float MaxAccuracy { get; }
    }
}