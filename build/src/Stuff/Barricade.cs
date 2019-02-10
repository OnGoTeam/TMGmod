using System;
using System.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.Particles;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [PublicAPI]
    public class Barricade:Block
    {
        public bool Anchored;
        public StateBinding AnchoredBinding = new StateBinding(nameof(Anchored));
        public float Hp;
        public StateBinding HpBinding = new StateBinding(nameof(Hp));
        public float ImpactSpeed;
        public StateBinding ImpactSpeedBinding = new StateBinding(nameof(ImpactSpeed));
        public float Duckcooldown;
        public StateBinding DcdBinding = new StateBinding(nameof(Duckcooldown));
        public Barricade(float x, float y) : base(x, y)
        {
            Anchored = true;
            Hp = 10f;
            thickness = 2f;
            physicsMaterial = PhysicsMaterial.Wood;
            _center = new Vec2(1f, 2f);
            _collisionOffset = new Vec2(-1f, -2f);
            _collisionSize = new Vec2(2f, 4f);
            _graphic = new Sprite(GetPath("barr"));
            flammable = 0.6f;
            _isStateObject = true;
            //if (!(owner is Duck duck)) return;
            //duck.clip.Add(this);
            //clip.Add(duck);
        }

        private bool CheckBlocks()
        {
            var blocks = Level.CheckLineAll<Block>(new Vec2(x, y + 2), new Vec2(x, y + 4));
            Anchored = false;
            foreach (var block in blocks)
            {
                if (block is Barricade barricade && !barricade.Anchored) continue;
                //else
                Anchored = true;
                
            }
            blocks = Level.CheckLineAll<Block>(new Vec2(x, y), new Vec2(x, y - 4));
            return Anchored || blocks.Any(block => block != this);
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            SFX.Play("woodHit");
            Damage(bullet.ammo.penetration * (bullet.ammo is ATShrapnel ? 2 : 1));
            ImpactSpeed = bullet.bulletSpeed * (x - hitPos.x);
            return base.Hit(bullet, hitPos);
        }

        private void Damage(float dValue)
        {
            var barricades = Level.CheckCircleAll<Barricade>(position, 10f);
            foreach (var barricade in barricades)
            {
                if (barricade == this || !(barricade.Hp >= 1f)) continue;
                barricade.Hp -= dValue;
                barricade.ImpactSpeed = ImpactSpeed;
            }

            Hp -= dValue;
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            ImpactSpeed = with.hSpeed;
            if (with is Duck duck && (duck.inputProfile.Down("SHOOT") || duck.sliding || duck.crouch) && Duckcooldown < 0)
            {
                SFX.Play("woodHit");
                Duckcooldown = 2.0f;
                Hp -= 4f;
                ImpactSpeed *= 2;
            }
            else if (Math.Abs(with.hSpeed) > 5f)
            {
                SFX.Play("woodHit");
                Hp -= Math.Abs(with.hSpeed) * 0.2f - 1f;
                Damage(Math.Abs(with.hSpeed) * 0.2f);
            }

            
            base.OnImpact(with, from);
        }

        public override void Update()
        {
            Duckcooldown -= 0.1f;
            if (Hp < 0f || !CheckBlocks())
            {
                Destroy();
            }
            base.Update();
            thickness = 0.2f * Hp;
        }

        protected override bool OnDestroy(DestroyType type0 = null)
        {
            SFX.Play("woodHit");
            var bbp = new BarrBetaPar(x, y)
            {
                hSpeed = ImpactSpeed * 0.9f + Rando.Float(-1f, 1f),
                vSpeed = Rando.Float(-1.5f, 1.5f)
            };
            Level.Add(bbp);
            Level.Remove(this);
            return true;
        }
    }
}