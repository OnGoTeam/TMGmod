namespace TMGmod.Core.DamageLogic
{
    // ReSharper disable once InconsistentNaming
    public interface IDamage
    {
        float DamageMean { get; }
        float DamageVariation { get; }
        float DistanceConvexity { get; }
        float AlphaDamage { get; }
    }
}
