using DuckGame;
using System;

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
            thickness = 1f;
            weight = 40f;
            throwSpeedMultiplier = 0f;
        }
        public override void Update()
        {
            var utkaEbutka = Level.CheckLine<Duck>(position, duck.position, duck);
            if (utkaEbutka != null)
            {
                while (Math.Abs(duck.hspeed * duck.hspeed + duck.vspeed * duck.vspeed) > 1f)
                {
                    duck.hspeed *= 0.9f;
                    duck.vspeed *= 0.9f;
                }
            }
            base.Update();
        }

        private void Damage(AmmoType at)
        {
            thickness = _hp < 950f ? _hp * 0.01f : 10000f;
            _hp -= at.penetration * 5f;
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