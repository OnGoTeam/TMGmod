using DuckGame;

namespace TMGmod.Core.AmmoTypes
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
            deadly = true;
            immediatelyDeadly = true;
            BulletDamage = 18f;
            DeltaDamage = 0.3f;
            AlphaDamage = 0.4f;
        }
    }
}