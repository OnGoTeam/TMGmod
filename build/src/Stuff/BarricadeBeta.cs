using System;
using System.Linq;
using DuckGame;
using TMGmod.Core.Particles;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    public class BarricadeBeta:Block
    {
        private bool _anchored;
        private float _hp;
        private float _impactSpeed;
        private float _duckcooldown;
        public BarricadeBeta(float x, float y) : base(x, y)
        {
            _anchored = true;
            _hp = 10f;
            thickness = 2f;
            physicsMaterial = PhysicsMaterial.Wood;
            center = new Vec2(1f, 2f);
            collisionOffset = new Vec2(-1f, -2f);
            collisionSize = new Vec2(2f, 4f);
            graphic = new Sprite(GetPath("barr"));
            flammable = 0.6f;
            //if (!(owner is Duck duck)) return;
            //duck.clip.Add(this);
            //clip.Add(duck);
        }

        private bool CheckBlocks()
        {
            var blocks = Level.CheckLineAll<Block>(new Vec2(x, y + 2), new Vec2(x, y + 4));
            _anchored = false;
            foreach (var block in blocks)
            {
                if (block is BarricadeBeta && !(block as BarricadeBeta)._anchored) continue;
                //else
                _anchored = true;
                
            }
            blocks = Level.CheckLineAll<Block>(new Vec2(x, y), new Vec2(x, y - 4));
            return _anchored || blocks.Any(block => block != this);
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            SFX.Play("woodHit");
            Damage(bullet.ammo.penetration * (bullet.ammo is ATShrapnel ? 2 : 1));
            _impactSpeed = bullet.hSpeed;
            return base.Hit(bullet, hitPos);
        }

        private void Damage(float dValue)
        {
            var barricades = Level.CheckCircleAll<BarricadeBeta>(position, 10f);
            foreach (var barricade in barricades)
            {
                if (barricade != this && barricade._hp >= 1f)
                {
                    barricade._hp -= dValue;
                }
            }

            _hp -= dValue;
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            if (with is Duck duck && duck.inputProfile.Down("SHOOT") && _duckcooldown < 0)
            {
                SFX.Play("woodHit");
                _duckcooldown = 2.0f;
                _hp -= 4f;
            }
            else if (Math.Abs(with.hSpeed) > 5f)
            {
                SFX.Play("woodHit");
                _hp -= Math.Abs(with.hSpeed) * 0.2f - 1f;
                Damage(Math.Abs(with.hSpeed) * 0.2f);
            }

            _impactSpeed = with.hSpeed * 2f;
            base.OnImpact(with, from);
        }

        public override void Update()
        {
            _duckcooldown -= 0.1f;
            if (_hp < 0f || !CheckBlocks())
            {
                SFX.Play("woodHit");
                var bbp = new BarrBetaPar(x, y)
                {
                    hSpeed = _impactSpeed + Rando.Float(-1f, 1f),
                    vSpeed = Rando.Float(-1.5f, 1.5f)
                };
                Level.Add(bbp);
                Level.Remove(this);
            }
            base.Update();
            thickness = 0.2f * _hp;
        }
    }
}