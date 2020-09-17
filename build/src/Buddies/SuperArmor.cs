#if DEBUG
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|Misc")]
    [PublicAPI]
    public class SuperArmor : Equipment, IDamage
    {
        private SpriteMap _sprite;
        private SpriteMap _spriteOver;
        private Sprite _pickupSprite;

        public SuperArmor(float xpos, float ypos) : base(xpos, ypos)
        {
            _hitPoints = 99f;
            _sprite = new SpriteMap("chestPlateAnim", 32, 32);
            _spriteOver = new SpriteMap("chestPlateAnimOver", 32, 32);
            _pickupSprite = new Sprite("chestPlatePickup");
            _pickupSprite.CenterOrigin();
            _graphic = _pickupSprite;
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(11f, 8f);
            _equippedCollisionOffset = new Vec2(-7f, -10f);
            _equippedCollisionSize = new Vec2(12f, 22f);
            _hasEquippedCollision = true;
            _center = new Vec2(8f, 8f);
            physicsMaterial = PhysicsMaterial.Metal;
            _equippedDepth = 2;
            _wearOffset = new Vec2(1f, 1f);
            _isArmor = true;
            _equippedThickness = 666f;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (_equippedDuck == null || bullet.owner == duck || !bullet.isLocal)
                return false;
            if (bullet.isLocal)
            {
                _hitPoints = _hitPoints - Damage.Calculate(bullet.ammo);
                if (_hitPoints < 0)
                {
                    duck.KnockOffEquipment(this, true, bullet);
                    Fondle(this, DuckNetwork.localConnection);
                    //kill owner
                }
            }
            if (bullet.isLocal && Network.isActive)
                _netTing.Play();
            Level.Add(MetalRebound.New(hitPos.x, hitPos.y, bullet.travelDirNormalized.x > 0 ? 1 : -1));
            for (var index = 0; index < 6; ++index)
                Level.Add(Spark.New(x, y, bullet.travelDirNormalized));
            return true;
        }
        public float Bulletdamage { get; }
        public float Deltadamage { get; }
    }
}
#endif