namespace TMGmod.Core.WClasses
{
    /// <summary>
    /// Interface for guns which have different Kforce unless it fired in <see cref="MaxDelaySmg"/> ago
    /// </summary>
    public interface IFirstKforce
    {
        /// <summary>
        /// Modified Kforce
        /// </summary>
        float KforceDSmg { get; }
        /// <summary>
        /// Delay from last shot
        /// </summary>
        int CurrDelaySmg { get; set; }
        /// <summary>
        /// Max delay from last shot
        /// </summary>
        int MaxDelaySmg { get; }
    }
}