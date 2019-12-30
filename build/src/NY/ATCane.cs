using DuckGame;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATCane : AmmoType
    {
        public ATCane()
        {
            bulletType = typeof(CandyCaneBullet);
            bulletLength = 3f;
            bulletSpeed = 15f;
            range = 500f;
            accuracy = 0.95f;
            sprite = new Sprite(Mod.GetPath<Core.TMGmod>("Holiday/Peppermint Classic"));
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