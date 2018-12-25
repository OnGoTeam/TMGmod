using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    public class MontagneShield : Holdable
    {
        private readonly SpriteMap _sprite;
        private float _hp;
        public StateBinding HpBinding = new StateBinding(nameof(_hp));
        public MontagneShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _hp = 1000f;
            _sprite = new SpriteMap(GetPath("Montagne"), 4, 23);
            graphic = _sprite;
            center = new Vec2(2f, 11.5f);
            collisionOffset = new Vec2(-2f, -11.5f);
            collisionSize = new Vec2(4f, 23f);
            physicsMaterial = PhysicsMaterial.Metal;
            thickness = 10f;
            weight = 8f;
            throwSpeedMultiplier = 0f;
        }

        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            SFX.Play(bullet.ammo.penetration < thickness ? "metalRebound" : "woodHit");
            Damage(bullet.ammo);
            return Hit(bullet, hitPos);
        }

        public override void Thrown()
        {
            if (duck == null) return;
            //else
            if (duck.inputProfile.Down("QUACK")) return;
            //else
            angleDegrees = 90f * offDir;
            collisionOffset = new Vec2(-11.5f, -2f);
            collisionSize = new Vec2(23f, 4f);
        }

        private void SetDefSett()
        {
            collisionOffset = new Vec2(-2f, -11.5f);
            collisionSize = new Vec2(4f, 23f);
        }

        private void Damage(AmmoType at)
        {
            thickness = _hp < 950f ? _hp * 0.01f : 10000f;
            _hp -= at.penetration * 5f;
            if (_hp <= 1000f)
            {
                _sprite.frame = 0;
            }

            if (_hp <= 600f)
            {
                _sprite.frame = 1;
            }

            if (_hp <= 300f)
            {
                _sprite.frame = 2;
            }

            if (_hp <= 200f)
            {
                _sprite.frame = 3;
            }
        }

        public override void Update()
        {
            if (duck == null)
            {
                /*if (grounded)
                {
                    angleDegrees = 90f * offDir;
                }*/
            }
            else
            {
                SetDefSett();
            }
            base.Update();
        }
    }
}