﻿using System;
using System.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.Particles;

namespace TMGmod.Stuff
{
    [BaggedProperty("CanSpawn", false)]
    [PublicAPI]
    public class Barricade : Block
    {
        public bool Anchored;
        public StateBinding AnchoredBinding = new(nameof(Anchored));
        public StateBinding DcdBinding = new(nameof(Duckcooldown));
        public float Duckcooldown;
        public float Hp;
        public StateBinding HpBinding = new(nameof(Hp));
        public float ImpactSpeed;
        public StateBinding ImpactSpeedBinding = new(nameof(ImpactSpeed));

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
        }

        private bool CheckBlocks()
        {
            var blocks = Level.CheckLineAll<Block>(new Vec2(x, y + 2), new Vec2(x, y + 4));
            Anchored = false;
            foreach (var block in blocks)
            {
                if (block is Barricade { Anchored: false }) continue;
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
            if (with is Duck duck && (duck.inputProfile.Down("SHOOT") || duck.sliding || duck.crouch) &&
                Duckcooldown < 0)
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
            if (Hp < 0f || !CheckBlocks()) Destroy();
            if (_destroyed) return;
            base.Update();
            thickness = 0.2f * Hp;
        }

        protected override bool OnDestroy(DestroyType type0 = null)
        {
            if (Level.activeLevel is not Editor)
            {
                SFX.Play("woodHit");
                if (isServerForObject)
                {
                    var bbp = new BarrBetaPar(x, y)
                    {
                        hSpeed = ImpactSpeed * 0.9f + Rando.Float(-1f, 1f),
                        vSpeed = Rando.Float(-1.5f, 1.5f),
                    };
                    Level.Add(bbp);
                }
            }

            Level.Remove(this);
            return true;
        }

        public override void Regenerate()
        {
        }
    }
}
