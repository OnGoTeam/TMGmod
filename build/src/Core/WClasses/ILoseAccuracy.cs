namespace TMGmod.Core.WClasses
{
    public interface ILoseAccuracy
    {
        float RhoAccuracyDmr { get; }  // regen
        float DeltaAccuracyDmr { get; }  // drain
    }
}