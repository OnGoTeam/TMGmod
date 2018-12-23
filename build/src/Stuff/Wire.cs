using System.Linq;
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
            graphic = new Sprite(GetPath("WireYes"));
            center = new Vec2(24f, 3f);
            collisionOffset = new Vec2(-24f, -3f);
            collisionSize = new Vec2(48f, 6f);
            thickness = 3f;
            weight = 40f;
            throwSpeedMultiplier = 0f;
        }
        public override void Update()
        {
            if (_hp < 0f) _hp = 0f;
            var probablyduck = Level.CheckRectAll<IAmADuck>(position + new Vec2(-24f, -3f), position + new Vec2(24f, 3f));
            foreach (var realyduck in probablyduck)
            {
                if (!(realyduck is Thing r1)) continue;
                //else
                r1.hSpeed *= 1f / (_hp / 26f + 1f);
                r1.vSpeed *= 1f / (_hp / 10f + 1f);
            }
            var wirelist = Level.CheckRectAll<Wire>(position + new Vec2(-24f, -3f), position + new Vec2(24f, 3f));
            if (wirelist.Any(wire => wire != this))
            {
                return;
            }
            base.Update();
        }
        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet.ammo.penetration < 10f) Damage(bullet.ammo);
            return Hit(bullet, hitPos);
        }
        private void Damage(AmmoType at)
        {
            if (at.penetration < 1.1f) return;
            _hp -= at.penetration;
            if (!(_hp < 1f)) return;
            //else
            thickness = 0.1f;
            graphic = new Sprite(GetPath("WireNot"));
        }
    }
}