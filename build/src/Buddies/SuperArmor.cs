#if DEBUG
using System.Globalization;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|Misc")]
    [PublicAPI]
    public class SuperArmor : Equipment
    {
        private SpriteMap _sprite;
        private SpriteMap _spriteOver;
        private readonly Sprite _pickupSprite;

        public override Vec2 collisionSize
        {
            get => _equippedDuck?.collisionSize ?? _collisionSize;
            set => _collisionSize = value;
        }

        public override Vec2 collisionOffset
        {
            get => _equippedDuck?.collisionOffset ?? _collisionOffset;
            set => _collisionOffset = value;
        }

        public override float top
        {
            get => _equippedDuck?.top ?? base.top;
            set => base.top = value;
        }

        public override float left
        {
            get => _equippedDuck?.left ?? base.left;
            set => base.left = value;
        }

        public override float bottom
        {
            get => _equippedDuck?.bottom ?? base.bottom;
            set => base.bottom = value;
        }

        public override float right
        {
            get => _equippedDuck?.right ?? base.right;
            set => base.right = value;
        }

        private const float HpMax = 99f;

        public SuperArmor(float xpos, float ypos) : base(xpos, ypos)
        {
            _hitPoints = HpMax;
            _sprite = new SpriteMap("chestPlateAnim", 32, 32);
            _spriteOver = new SpriteMap("chestPlateAnimOver", 32, 32);
            _pickupSprite = new Sprite("chestPlatePickup");
            _pickupSprite.CenterOrigin();
            _graphic = _pickupSprite;
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(11f, 8f);
            _equippedCollisionOffset = new Vec2(-6f, -11f);
            _equippedCollisionSize = new Vec2(12f, 22f);
            _hasEquippedCollision = true;
            _center = new Vec2(8f, 8f);
            physicsMaterial = PhysicsMaterial.Duck;
            _equippedDepth = 2;
            _wearOffset = new Vec2(0, 0);
            _isArmor = true;
            _equippedThickness = 666f;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (_equippedDuck == null || bullet.owner == _equippedDuck || !bullet.isLocal)
                return false;
            _hitPoints -= Damage.Calculate(bullet);
            if (_hitPoints < 0)
            {
                var equippedDuck1 = _equippedDuck;
                equippedDuck1.KnockOffEquipment(this, true, bullet);
                Fondle(this, DuckNetwork.localConnection);
                equippedDuck1.Destroy(new DTShot(bullet));
                //kill owner
            }
            if (bullet.isLocal && Network.isActive)
                _netTing.Play();
            Level.Add(MetalRebound.New(hitPos.x, hitPos.y, bullet.travelDirNormalized.x > 0 ? 1 : -1));
            for (var index = 0; index < 6; ++index)
                Level.Add(Spark.New(x, y, bullet.travelDirNormalized));
            return true;
        }

        public override void Draw()
        {
            Graphics.DrawString(_hitPoints.ToString(CultureInfo.InvariantCulture), position + new Vec2(0, -16), Color.GreenYellow);
            Graphics.DrawRect(rectangle, new Color(255, 0, 0, 128));
            if (_equippedDuck != null) Graphics.DrawRect(_equippedDuck.rectangle, new Color(0, 0, 255, 128));
        }
    }
}
#endif