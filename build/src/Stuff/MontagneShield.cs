using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Stuff
{
    [EditorGroup("TMM|STUFF")]
    public class MontagneShield : Holdable
    {
        private readonly SpriteMap _sprite;
        private int _hp;
        public MontagneShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _hp = 30;
            _sprite = new SpriteMap(GetPath("Montagne"), 4, 23);
            graphic = _sprite;
            center = new Vec2(2f, 11.5f);
            collisionOffset = new Vec2(-2f, -11.5f);
            collisionSize = new Vec2(4f, 23f);
            physicsMaterial = PhysicsMaterial.Metal;
            thickness = 4f;
            weight = 8f;
            throwSpeedMultiplier = 0f;
        }

        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            SFX.Play(bullet.ammo.penetration < thickness ? "metalRebound" : "woodHit");
            Damage();
            return Hit(bullet, hitPos);
        }

        public override void Thrown()
        {
            angleDegrees = 90f * offDir;
            collisionOffset = new Vec2(-11.5f, -2f);
            collisionSize = new Vec2(23f, 4f);
        }

        private void SetDefSett()
        {
            collisionOffset = new Vec2(-2f, -11.5f);
            collisionSize = new Vec2(4f, 23f);
        }

        private void Damage()
        {
            _hp--;
            if (_hp <= 30)
            {
                _sprite.frame = 0;
                thickness = 4f;
            }

            if (_hp <= 20)
            {
                _sprite.frame = 1;
                thickness = 3f;
            }

            if (_hp <= 10)
            {
                _sprite.frame = 2;
                thickness = 1f;
            }

            if (_hp <= 5)
            {
                _sprite.frame = 3;
                thickness = 0.2f;
            }

            if (_hp < 0)
            {
                thickness = 0f;
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

                if (_hp < 0)
                {
                    Level.Remove(this);
                }
            }
            else
            {
                SetDefSett();
            }
            base.Update();
        }
    }
}