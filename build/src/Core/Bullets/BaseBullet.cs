using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.Bullets
{
    public class BaseBullet : Bullet
    {
        private float _damagePortion = 1f;
        [UsedImplicitly]
        public BaseBullet(
            float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false,
            float distance = -1, bool tracer = false, bool network = true
        ) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public void LoseDamage() => _damagePortion *= .5f;

        public float DamagePortion() => _damagePortion;
    }
}
