
using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public interface IDamage
    {
        float Bulletdamage { get; }
        float Deltadamage{ get; }
        //float Distancefactor { get; } - падение урона с дистанцией
    }
    public static class Damage
    {
        private static float GetDamage(AmmoType ammo)
        {
            return ammo is IDamage damage ? damage.Bulletdamage : 50f;
        }

        private static float GetDelta(AmmoType ammo)
        {
            return ammo is IDamage damage ? damage.Deltadamage : 1f;
        }
        public static float Calculate(AmmoType ammo)
        {
            var delta = GetDelta(ammo);
            return Rando.Float(1-delta, 1+delta) * GetDamage(ammo);
        }
    }
}