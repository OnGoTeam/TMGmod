﻿using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [UsedImplicitly]
    public class MontagneShield : Holdable, IPlatform, IPathNodeBlocker
    {
        private const float HpMax = 250f;

        private const float Hp1 = 0.9f * HpMax;
        private readonly SpriteMap _sprite;

        [UsedImplicitly] public float Hp = HpMax;

        [UsedImplicitly] public StateBinding HpBinding = new(nameof(Hp));

        public MontagneShield(float xpos, float ypos) : base(xpos, ypos)
        {
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
            _holdOffset = new Vec2(0, -0.5f);
        }

        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            SFX.Play(bullet.ammo.penetration < thickness ? "metalRebound" : "woodHit");
            Damage(bullet.ammo);
            var res = Hit(bullet, hitPos);
            return !((hitPos - Offset(new Vec2(0, -7.5f))).length < 3.354102f) && res;
        }

        public override void Thrown()
        {
            if (duck?.inputProfile.Down("QUACK") != false) return;
            //else
            angleDegrees = 90f * offDir;
            collisionOffset = new Vec2(-11.5f, -2f);
            collisionSize = new Vec2(23f, 4f);
        }

        private void Reset()
        {
            collisionOffset = new Vec2(-2f, -11.5f);
            collisionSize = new Vec2(4f, 23f);
        }

        private void Damage(AmmoType at)
        {
            thickness = Hp < Hp1 ? Hp * 0.04f : 10000f;
            Hp -= at is IHeavyAmmoType ? HpMax * 0.49f : at.penetration * 5f;
            if (Hp <= HpMax)
                _sprite.frame = 0;
            if (Hp <= HpMax * 0.75f)
                _sprite.frame = 1;
            if (Hp <= HpMax * 0.5f)
                _sprite.frame = 2;
            if (Hp <= HpMax * 0.25f)
                _sprite.frame = 3;
            if (duck is { } && duck.holdObject == this) return;
            if (Rando.Float(0, 1) > (at is IHeavyAmmoType ? 0.5f : 0.1f)) return;
            angleDegrees = 90f * offDir;
            collisionOffset = new Vec2(-11.5f, -2f);
            collisionSize = new Vec2(23f, 4f);
            sleeping = false;
        }

        public override void Impact(MaterialThing with, ImpactedFrom from, bool solidImpact)
        {
            var doblock = Level.CheckRect<ShieldBlockAll>(new Vec2(-1000, -1000), new Vec2(1000, 1000)) is { };
            if ((collisionSize.x < 5f && (doblock || with is IAmADuck) && with is not (IDontMove or Block) &&
                 from == ImpactedFrom.Left) || from == ImpactedFrom.Right)
            {
                if (duck is null && Math.Abs(with.hSpeed) * with.weight > 40f)
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

        public override void Update()
        {
            var hspd = duck?.hSpeed ?? hSpeed;
            var dvecx = hspd * 3;
            var hit1 = topLeft + new Vec2(Math.Min(dvecx, 0), 0);
            var hit2 = bottomRight + new Vec2(Math.Max(dvecx, 0), 0);
            foreach (var fire in Level.CheckRectAll<SmallFire>(hit1, hit2)) fire.hSpeed = hspd;
            var doblock = Level.CheckRect<ShieldBlockAll>(new Vec2(-1000, -1000), new Vec2(1000, 1000)) is { };
            if (collisionSize.x < 5f)
                foreach (var thing in Level.CheckRectAll<MaterialThing>(hit1, hit2))
                {
                    if (thing == duck || thing == this || thing is IDontMove or Block or Teleporter) continue;
                    if (!(thing is IAmADuck || doblock)) continue;
                    //else
                    thing.hSpeed = hspd;
                    thing.x += Maths.Clamp(Math.Abs(thing.x - x) > 0.01f ? (thing is Duck ? 2 : 4) / (thing.x - x) : 0, -2f, 2f);
                    if (Math.Abs(hspd) < 0.1f) continue;
                    //else
                    var hvk = Math.Abs(thing.x - x) / 2f;
                    hvk = Math.Min(hvk, 1);
                    if (duck is { }) duck.hSpeed *= hvk;
                    else hSpeed *= hvk;
                }

            if (duck is { }) Reset();

            base.Update();
        }
    }
}
