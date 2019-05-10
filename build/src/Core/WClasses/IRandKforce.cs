namespace TMGmod.Core.WClasses
{
    /// <summary>
    /// Interface for guns which have random Kforce
    /// </summary>
    public interface IRandKforce
    {
        /// <summary>
        /// min Kforce
        /// </summary>
        float Kforce1Lmg { get; }
        /// <summary>
        /// max Kforce
        /// </summary>
        float Kforce2Lmg { get; }
    }
}