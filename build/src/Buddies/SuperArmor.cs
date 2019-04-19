#if DEBUG
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|Misc")]
    [PublicAPI]
    public class SuperArmor:ChestPlate
    {
        public SuperArmor(float xpos, float ypos) : base(xpos, ypos)
        {
            _equippedThickness = 3f;
            _hitPoints = 100f;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (_equippedDuck == null || bullet.owner == duck || !bullet.isLocal)
                return false;
            if (bullet.isLocal)
            {
                _hitPoints -= bullet.ammo.penetration;
                if (_hitPoints < 0)
                {
                    duck.KnockOffEquipment(this, true, bullet);
                    Fondle(this, DuckNetwork.localConnection);
                }
            }
            if (bullet.isLocal && Network.isActive)
                _netTing.Play();
            Level.Add(MetalRebound.New(hitPos.x, hitPos.y, bullet.travelDirNormalized.x > 0 ? 1 : -1));
            for (var index = 0; index < 6; ++index)
                Level.Add(Spark.New(x, y, bullet.travelDirNormalized));
            return thickness > bullet.ammo.penetration;
        }
    }
}
#endif