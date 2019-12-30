using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATCane : BaseAmmoTypeT
    {
        public ATCane(Thing cc)
        {
            bulletType = typeof(CandyCaneBullet);
            sprite = cc.graphic;
            bulletLength = 3f;
            bulletSpeed = 15f;
        }

        public override void OnHit(bool destroyed, Bullet b)
        {
            base.OnHit(destroyed, b);
            if (destroyed && b.firedFrom is CandyCane c)
            {
                c.Drop(b.x, b.y);
            }
        }
    }
}
