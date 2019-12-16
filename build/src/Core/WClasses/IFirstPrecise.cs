namespace TMGmod.Core.WClasses
{
    /// <summary>
    /// Interface for guns which have different Kforce unless it fired in <see cref="MaxAccuracy"/> ago
    /// </summary>
    public interface IFirstPrecise
    {
        /// <summary>
        /// Delay from last shot
        /// </summary>
        int CurrDelay { get; set; }
        /// <summary>
        /// Max delay from last shot
        /// </summary>
        int MaxDelayFp { get; }
        /// <summary>
        /// Modified accuracy
        /// </summary>
        float MaxAccuracy { get; }
    }
}