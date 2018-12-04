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
        private bool _right;
        private float _duckcooldown;
        public BarricadeBeta(float x, float y) : base(x, y)
        {
            _anchored = true;
            _hp = 10;
            thickness = 3f;
            physicsMaterial = PhysicsMaterial.Wood;
            center = new Vec2(1f, 2f);
            collisionOffset = new Vec2(-1f, -2f);
            collisionSize = new Vec2(2f, 4f);
            graphic = new Sprite(GetPath("barr"));
            flammable = 0.6f;
        }

        private bool CheckBlocks()
        {
            var blocks = Level.CheckLineAll<Block>(new Vec2(x, y + 2), new Vec2(x, y + 4));
            _anchored = false;
            foreach (var block in blocks)
            {
                if (!(block is BarricadeBeta) || (block as BarricadeBeta)._anchored)
                    _anchored = true;
            }
            blocks = Level.CheckLineAll<Block>(new Vec2(x, y), new Vec2(x, y - 4));
            return _anchored || blocks.Any(block => block != this);
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            SFX.Play("woodHit");
            Damage();
            _right = (hitPos - position).x > 0;
            return base.Hit(bullet, hitPos);
        }

        private void Damage()
        {
            var barricades = Level.CheckCircleAll<BarricadeBeta>(position, 10f);
            foreach (var barricade in barricades)
            {
                if (barricade != this && barricade._hp >= 1f)
                {
                    barricade._hp -= 1f;
                }
            }

            _hp -= 1f;
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            _right = from == ImpactedFrom.Left;
            if (with is Duck /*duck && duck.inputProfile.Down("SHOOT")*/ && _duckcooldown < 0)
            {
                SFX.Play("woodHit");
                _duckcooldown = 1.0f;
                _hp -= 2f;
            }
            else if (Math.Abs(with.hSpeed) > 5f)
            {
                SFX.Play("woodHit");
                _hp -= Math.Abs(with.hSpeed) * 0.2f - 1f;
                Damage();
            }
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
                    hSpeed = (_right ? -4f : 4f) + Rando.Float(-1f, 1f),
                    vSpeed = Rando.Float(-1.5f, 1.5f)
                };
                Level.Add(bbp);
                Level.Remove(this);
            }
            base.Update();
        }
    }
}