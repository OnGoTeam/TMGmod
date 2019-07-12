using System;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    /// <summary>
    /// Holdable shield protecting from bullets and impacts
    /// </summary>
    [EditorGroup("TMG|Misc")]
    [PublicAPI]
    public class MontagneShield : Holdable, IPlatform, IPathNodeBlocker
    {
        private readonly SpriteMap _sprite;
        /// <summary>
        /// hitpoints
        /// </summary>
        public float Hp = 250f;
        /// <summary>
        /// max <see cref="Hp"/>
        /// </summary>
        public float HpMax = 250f;
        /// <summary>
        /// Absolute invincibility limit
        /// </summary>
        public float Hp1;
        /// <summary>
        /// <see cref="Hp"/> syncing
        /// </summary>
        public StateBinding HpBinding = new StateBinding(nameof(Hp));

        /// <inheritdoc />
        public MontagneShield(float xpos, float ypos) : base(xpos, ypos)
        {
            Hp1 = Hp * 0.9f;
            _sprite = new SpriteMap(GetPath("Montagne"), 4, 23);
            _graphic = _sprite;
            _center = new Vec2(2f, 11.5f);
            _collisionOffset = new Vec2(-2f, -11.5f);
            _collisionSize = new Vec2(4f, 23f);
            physicsMaterial = PhysicsMaterial.Metal;
            thickness = 10f;
            _weight = 8f;
            throwSpeedMultiplier = 0f;
            _canRaise = false;
            flammable = 0;
        }

        /// <inheritdoc />
        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            SFX.Play(bullet.ammo.penetration < thickness ? "metalRebound" : "woodHit");
            Damage(bullet.ammo);
            return Hit(bullet, hitPos);
        }

        /// <inheritdoc />
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
            thickness = Hp < Hp1 ? Hp * 0.04f : 10000f;
            Hp -= at.penetration * 5f;
            if (Hp <= HpMax)
            {
                _sprite.frame = 0;
            }

            if (Hp <= HpMax * 0.75f)
            {
                _sprite.frame = 1;
            }

            if (Hp <= HpMax * 0.5f)
            {
                _sprite.frame = 2;
            }

            if (Hp <= HpMax * 0.25f)
            {
                _sprite.frame = 3;
            }
        }

        /// <inheritdoc />
        public override void Impact(MaterialThing with, ImpactedFrom from, bool solidImpact)
        {
            var doblock = Level.CheckRect<ShieldBlockAll>(new Vec2(-1000, -1000), new Vec2(1000, 1000)) != null;
            if (collisionSize.x < 5f && (doblock || with is IAmADuck) && !(with is IDontMove || with is Block) && @from == ImpactedFrom.Left || from == ImpactedFrom.Right)
            {
                if (duck == null && Math.Abs(with.hSpeed) * with.weight > 40f)
                {
                    angleDegrees = 90f * offDir;
                    collisionOffset = new Vec2(-11.5f, -2f);
                    collisionSize = new Vec2(23f, 4f);
                    sleeping = false;
                }
                with.hSpeed = hSpeed;
            }
            base.Impact(with, from, solidImpact);
        }

        /// <inheritdoc />
        public override void Update()
        {
            var hspd = duck?.hSpeed ?? hSpeed;
            var dvecx = hspd * 3;
            var hit1 = topLeft + new Vec2(Math.Min(dvecx, 0), 0);
            var hit2 = bottomRight + new Vec2(Math.Max(dvecx, 0), 0);
            foreach (var fire in Level.CheckRectAll<SmallFire>(hit1, hit2))
            {
                fire.hSpeed = hspd;
            }
            var doblock = Level.CheckRect<ShieldBlockAll>(new Vec2(-1000, -1000), new Vec2(1000, 1000)) != null;
            if (collisionSize.x < 5f) foreach (var thing in Level.CheckRectAll<MaterialThing>(hit1, hit2))
            {
                if (thing == duck || thing == this || thing is IDontMove || thing is Block || thing is Teleporter) continue;
                if (!(thing is IAmADuck || doblock)) continue;
                thing.hSpeed = hspd;
                var dx = Math.Abs(thing.x - x) > 0.01f ? (thing is Duck? 2: 4) / (thing.x - x): 0;
                dx = Math.Min(2, dx);
                dx = Math.Max(-2, dx);
                thing.x += dx;
                if (Math.Abs(hspd) < 0.1f) continue;
                //else
                var hvk = Math.Abs(thing.x - x) / 2f;
                hvk = Math.Min(hvk, 1);
                if (duck != null) duck.hSpeed *= hvk;
                else hSpeed *= hvk;
            }
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

        /*public override void Touch(MaterialThing with)
        {
            base.Touch(with);
        }*/
    }
}