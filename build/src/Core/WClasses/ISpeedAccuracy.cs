namespace TMGmod.Core.WClasses

{
    /// <summary>
    /// Interface for guns which have accuracy depending on speed
    /// </summary>
    public interface ISpeedAccuracy
    {
        /// <summary>
        /// Acc coefficient
        /// </summary>
        float MuAccuracySr { get; }
        /// <summary>
        /// Acc-speed coefficient
        /// </summary>
        float LambdaAccuracySr { get; }
    }
}