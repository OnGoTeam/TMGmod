namespace TMGmod.Core.WClasses
{
    /// <summary>
    /// Interface for guns which have Kforce depending on speed
    /// </summary>
    public interface IHspeedKforce
    {
        float Kforce1Ar { get; }
        float Kforce2Ar { get; }
    }
}