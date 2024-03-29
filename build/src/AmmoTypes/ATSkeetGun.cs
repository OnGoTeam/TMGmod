using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSkeetGun : BaseAmmoType
    {
        public ATSkeetGun()
        {
            range = 250f;
            accuracy = 0.9f;
            penetration = 1.5f;
            bulletColor = new Color(255, 0, 0);
            bulletSpeed = 50f;
            DamageMean = 18f;
            DamageVariation = 0.3f;
            AlphaDamage = 0.55f;
        }
    }
}
