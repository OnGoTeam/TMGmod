using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATCane : BaseAmmoTypeT
    {
        public ATCane(Sprite cc)
        {
            bulletType = typeof(CandyCaneBullet);
            sprite = cc;
            bulletLength = 3f;
            bulletSpeed = 15f;
            range = 500f;
        }

        public override void OnHit(bool destroyed, Bullet b)
        {
            base.OnHit(destroyed, b);
            if (b is CandyCaneBullet cb && destroyed && b.firedFrom is CandyCane c)
            {
                c.Drop(cb.end - cb.travelDirNormalized * cb.bulletSpeed);
            }
        }
    }
}