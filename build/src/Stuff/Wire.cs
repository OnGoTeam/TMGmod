using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    public class Wire : PhysicsObject
    {
        private float _hp;
        public Wire(float xpos, float ypos) : base(xpos, ypos)
        {
            _hp = 25f;
            graphic = new SpriteMap(GetPath("WireYes"), 48, 16);
            center = new Vec2(24f, 2f);
            collisionOffset = new Vec2(-24f, -2f);
            collisionSize = new Vec2(48f, 16f);
            thickness = 125f;
            weight = 40f;
            throwSpeedMultiplier = 0f;
        }
        public override void Update()
        {
            var probablyduck = Level.CheckRectAll<Duck>(position + new Vec2(-24f, -3f), position + new Vec2(24f, 3f));
            foreach (var realyduck in probablyduck)
            {
                realyduck.hSpeed *= 0.5f * (1f/(26f-_hp));
                realyduck.vSpeed *= 0.8f * (1f/(26f-_hp));
            }
            base.Update();
        }
        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            if ((bullet.ammo.penetration < 1f) || (bullet.ammo.penetration > 95f)) Damage(bullet.ammo);
            return Hit(bullet, hitPos);
        }
        private void Damage(AmmoType at)
        {
            _hp -= at.penetration * 2f;
            if (_hp > 1f)
            {
                graphic = new SpriteMap(GetPath("WireYes"), 48, 16);
            }
            if (_hp < 1f)
            {
                graphic = new SpriteMap(GetPath("WireNot"), 48, 16);
            }
        }
    }
}