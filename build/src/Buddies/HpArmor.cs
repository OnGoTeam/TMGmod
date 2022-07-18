#if FEATURES_1_2
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|Misc")]
    [UsedImplicitly]
    public class HpArmor : Equipment
    {
        private readonly float _hpMax;

        [UsedImplicitly] public StateBinding HitPointsBinding = new StateBinding(nameof(_hitPoints));

        public HpArmor(float xpos, float ypos, float hpMax = 99f) : base(xpos, ypos)
        {
            _hpMax = hpMax;
            _hitPoints = _hpMax;
            _collisionOffset = new Vec2(-4f, -4f);
            _collisionSize = new Vec2(8f, 8f);
            _equippedCollisionOffset = new Vec2(-12f, -12f);
            _equippedCollisionSize = new Vec2(24f, 24f);
            _hasEquippedCollision = true;
            _center = new Vec2(8f, 8f);
            physicsMaterial = PhysicsMaterial.Duck;
            _equippedDepth = 2;
            _wearOffset = new Vec2(0, 0);
            _isArmor = true;
            _equippedThickness = 666f;
            canPickUp = false;
            _translucent = true;
        }

        public override Vec2 collisionSize
        {
            get => EquippedDuck()?.collisionSize ?? _collisionSize;
            set => _collisionSize = value;
        }

        public override Vec2 collisionOffset
        {
            get => EquippedDuck()?.collisionOffset ?? _collisionOffset;
            set => _collisionOffset = value;
        }

        private static Vec2 Orthonormal(Vec2 vec)
        {
            return new Vec2(vec.y, -vec.x).normalized;
        }

        private static bool LineIntersectsSegment(
            Vec2 origin,
            Vec2 direction,
            Vec2 pos0,
            Vec2 pos1
        )
        {
            return Math.Sign(Vec2.Dot(Orthonormal(direction), (pos0 - origin).normalized))
                   *
                   Math.Sign(Vec2.Dot(Orthonormal(direction), (pos1 - origin).normalized))
                   <
                   0;
        }

        private static bool PlaneIntersectsSegment(
            Vec2 origin,
            Vec2 direction,
            Vec2 pos0,
            Vec2 pos1
        )
        {
            return Math.Sign(Vec2.Dot(direction, (pos0 - origin).normalized)) > 0
                   ||
                   Math.Sign(Vec2.Dot(direction, (pos1 - origin).normalized)) > 0;
        }

        private static bool RayIntersectsSegment(
            Vec2 origin,
            Vec2 direction,
            Vec2 pos0,
            Vec2 pos1
        )
        {
            return LineIntersectsSegment(origin, direction, pos0, pos1)
                   &&
                   PlaneIntersectsSegment(origin, direction, pos0, pos1);
        }

        private static bool RayIntersectsThing(
            Vec2 origin,
            Vec2 direction,
            Thing thing
        )
        {
            if (thing is Ragdoll ragdoll)
            {
                return RayIntersectsThing(origin, direction, ragdoll.part1)
                       ||
                       RayIntersectsThing(origin, direction, ragdoll.part2)
                       ||
                       RayIntersectsThing(origin, direction, ragdoll.part3);
            }

            return RayIntersectsSegment(origin, direction, thing.topLeft, thing.topRight)
                   ||
                   RayIntersectsSegment(origin, direction, thing.topRight, thing.bottomRight)
                   ||
                   RayIntersectsSegment(origin, direction, thing.bottomRight, thing.bottomLeft)
                   ||
                   RayIntersectsSegment(origin, direction, thing.bottomLeft, thing.topLeft);
        }

        private bool QHit(Vec2 hitPos, Vec2 travelDirNormalized)
        {
            return RayIntersectsThing(hitPos, travelDirNormalized, EquippedDuck());
        }

        private bool QLocalHit(Thing bullet)
        {
            return _equippedDuck != null && bullet.owner != _equippedDuck && bullet.isLocal;
        }

        private bool QHit(Bullet bullet, Vec2 hitPos)
        {
            return QLocalHit(bullet) && QHit(hitPos, bullet.travelDirNormalized);
        }

        private void DecorativeHit(Bullet bullet, Vec2 hitPos)
        {
            Level.Add(MetalRebound.New(hitPos.x, hitPos.y, bullet.travelDirNormalized.x > 0 ? 1 : -1));
            for (var index = 0; index < 6; ++index)
                Level.Add(Spark.New(x, y, bullet.travelDirNormalized));
        }

        private bool Kill(Bullet bullet)
        {
            var equippedDuck1 = _equippedDuck;
            equippedDuck1.invincible = false;
            equippedDuck1.KnockOffEquipment(this, true, bullet);
            Fondle(this, DuckNetwork.localConnection);
            equippedDuck1.Destroy(new DTShot(bullet));
            Level.Remove(this);
            return equippedDuck1.thickness > bullet.ammo.penetration;
        }

        private void Damage(float damage)
        {
            if (isServerForObject) _hitPoints -= damage;
#if DEBUG
            StringMarker.Show(position, damage.ToString(CultureInfo.InvariantCulture));
#endif
        }

        private void Slowdown()
        {
            EquippedDuck().hSpeed *= 0.25f;
        }

        private void Damage(Bullet bullet)
        {
            Slowdown();
            Damage(DamageImplementation.Calculate(bullet));
        }

        private static void ShowMarkers(Bullet bullet, Vec2 hitPos)
        {
            DotMarker.Show(bullet.end);
            StrokeMarker.Show(hitPos, bullet.end);
        }

        private bool Broken()
        {
            return _hitPoints < 0;
        }

        private bool RealHit(Bullet bullet, Vec2 hitPos)
        {
            Damage(bullet);
            DecorativeHit(bullet, hitPos);
            bullet.hitArmor = true;
            return !Broken() || Kill(bullet);
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            ShowMarkers(bullet, hitPos);
            return QHit(bullet, hitPos) && RealHit(bullet, hitPos);
        }

        private Thing EquippedDuck()
        {
            return _equippedDuck is null
                ? null
                : _equippedDuck.ragdoll != null
                    ? (Thing)_equippedDuck.ragdoll
                    : _equippedDuck;
        }

        public override void Draw()
        {
#if DEBUG
            Graphics.DrawRect(rectangle, new Color(255, 0, 0, 128), filled: false);
#endif
            if (EquippedDuck() == null) return;
            var start = (EquippedDuck().topLeft + EquippedDuck().topRight) / 2 + new Vec2(-32, 0);
            Graphics.DrawRect(start, start + new Vec2(64, -8), Color.Red, 0.0f);
            Graphics.DrawRect(
                start,
                start + new Vec2(0, -8) + new Vec2(64, 0) * Maths.Clamp(_hitPoints / _hpMax, 0f, 1f),
                Color.Green, 0.1f
            );
#if DEBUG
            Graphics.DrawString(
                _hitPoints.ToString(CultureInfo.InvariantCulture),
                start + new Vec2(64, -8),
                Color.GreenYellow
            );
            Graphics.DrawRect(EquippedDuck().rectangle, new Color(0, 0, 255, 128));
#endif
        }

        public override bool Destroy(DestroyType type1 = null)
        {
            return base.Destroy(type1);
        }

#if DEBUG
        private readonly List<string> _log = new List<string>();
        private const int MaxLogLen = 10;
#endif

        public override void Update()
        {
#if DEBUG
            if (duck != null)
            {
                foreach (var key in new[] { "UP", "DOWN", "LEFT", "RIGHT", "QUACK", "RAGDOLL" })
                {
                    if (duck.inputProfile.Pressed(key)) _log.Add(key);
                    if (_log.Count > MaxLogLen)
                        _log.RemoveRange(0, _log.Count - MaxLogLen);
                }

                if (
                    _log.SequenceEqual(
                        new[]
                        {
                            "UP",
                            "UP",
                            "DOWN",
                            "DOWN",
                            "LEFT",
                            "RIGHT",
                            "LEFT",
                            "RIGHT",
                            "QUACK",
                            "RAGDOLL",
                        }
                    )
                ) _hitPoints += 4.0765666406789425f;
            }
#endif
            base.Update();
            if (_equippedDuck is null)
            {
                Level.Remove(this);
                return;
            }

            _hitPoints = Math.Min(_hitPoints, Math.Max(.1f * _hpMax, _hitPoints - _equippedDuck.burnt));
        }

        public override void UnEquip()
        {
            if (_equippedDuck != null)
            {
                _equippedDuck.invincible = false;
                _equippedDuck.Destroy(new DTCrush(this));
            }

            base.UnEquip();
        }
    }
}
#endif
