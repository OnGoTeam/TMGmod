namespace TMGmod.Core.WClasses

{
    public interface ISpeedAccuracy
    {
        float SpeedAccuracyThreshold { get; }
        float SpeedAccuracyHorizontal { get; }
        float SpeedAccuracyVertical { get; }
    }
}
