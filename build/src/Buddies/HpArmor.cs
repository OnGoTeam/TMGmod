#if FEATURES_1_3
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.DamageLogic;

namespace TMGmod.Buddies
{
    [BaggedProperty("canSpawn", false)]
    [UsedImplicitly]
    public class HpArmor : Equipment
    {
        public float HpMax;

        [UsedImplicitly] public StateBinding HitPointsBinding = new StateBinding(nameof(_hitPoints));
        [UsedImplicitly] public StateBinding HpMaxBinding = new StateBinding(nameof(HpMax));

        public HpArmor(float xpos, float ypos, float hpMax = 99f) : base(xpos, ypos)
        {
            HpMax = hpMax;
            _hitPoints = HpMax;
            _collisionOffset = new Vec2(-4f, -4f);
            _collisionSize = new Vec2(8f, 8f);
            _equippedCollisionOffset = new Vec2(-16f, -24f);
            _equippedCollisionSize = new Vec2(32f, 48f);
            _hasEquippedCollision = true;
            _center = new Vec2(8f, 8f);
            physicsMaterial = PhysicsMaterial.Duck;
            _equippedDepth = 2;
            _wearOffset = new Vec2(0, 0);
            _isArmor = true;
            _equippedThickness = 0f;
            canPickUp = false;
            _translucent = true;
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

            var topLeft = thing.topLeft + new Vec2(-.5f, -.5f);
            var topRight = thing.topRight + new Vec2(+.5f, -.5f);
            var bottomRight = thing.bottomRight + new Vec2(+.5f, +.5f);
            var bottomLeft = thing.bottomLeft + new Vec2(-.5f, +.5f);

            return RayIntersectsSegment(origin, direction, topLeft, topRight)
                   ||
                   RayIntersectsSegment(origin, direction, topRight, bottomRight)
                   ||
                   RayIntersectsSegment(origin, direction, bottomRight, bottomLeft)
                   ||
                   RayIntersectsSegment(origin, direction, bottomLeft, topLeft);
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

        private void NetworkKill(DestroyType dtype)
        {
            var equippedDuck1 = _equippedDuck;
            equippedDuck1.invincible = false;
            equippedDuck1.KnockOffEquipment(this);
            Fondle(this, DuckNetwork.localConnection);
            equippedDuck1.Destroy(dtype);
            Level.Remove(this);
        }

        private void Damage(float damage)
        {
            _hitPoints -= damage;
#if DEBUG
            StringMarker.Show(position, damage.ToString(CultureInfo.InvariantCulture));
#endif
        }

        private void Slowdown()
        {
            if (EquippedDuck() != null)
                EquippedDuck().hSpeed *= 0.25f;
        }

        private void DoDamage(float damage)
        {
            Slowdown();
            Damage(damage);
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

        public void NetworkHit(float damage)
        {
            DoDamage(damage);
            if (Broken() && EquippedDuck() != null)
                NetworkKill(new DTCrush(this));
        }

        private bool RealHit(Bullet bullet, Vec2 hitPos)
        {
            DecorativeHit(bullet, hitPos);
            bullet.hitArmor = true;
            var damage = DamageImplementation.Calculate(bullet);
            if (isServerForObject)
                DoDamage(damage);
            else if (Network.isActive)
                Send.Message(new NmHpDamage(this, damage));
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
                start + new Vec2(0, -8) + new Vec2(64, 0) * Maths.Clamp(_hitPoints / HpMax, 0f, 1f),
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
                ) _hitPoints *= 1.0040765666406789425f;

                if (
                    _log.SequenceEqual(
                        new[]
                        {
                            "UP",
                            "LEFT",
                            "DOWN",
                            "RIGHT",
                            "UP",
                            "RIGHT",
                            "DOWN",
                            "LEFT",
                            "UP",
                            "QUACK",
                        }
                    )
                ) _hitPoints = HpMax;
            }
#endif
            if (_equippedDuck?.ragdoll != null)
            {
                var r = Bounding(
                    _equippedDuck.ragdoll.part1.rectangle,
                    Bounding(
                        _equippedDuck.ragdoll.part2.rectangle,
                        _equippedDuck.ragdoll.part3.rectangle
                    )
                );
                var v = _equippedDuck.ragdoll.velocity * 2;
                var r2 = new Rectangle(r.tl + v, r.br + v);
                r = Bounding(r, r2);
                _equippedCollisionSize = new Vec2(r.width, r.height);
                _equippedCollisionOffset = r.tl - _equippedDuck.skeleton.upperTorso.position;
            }
            else if (_equippedDuck?.skeleton != null)
            {
                _equippedCollisionSize = _equippedDuck.collisionSize;
                _equippedCollisionOffset = _equippedDuck.topLeft - _equippedDuck.skeleton.upperTorso.position;
            }

            base.Update();
            if (_equippedDuck is null || _equippedDuck.dead)
            {
                Level.Remove(this);
                return;
            }

            _hitPoints = Math.Min(_hitPoints, Math.Max(.1f * HpMax, _hitPoints - _equippedDuck.burnt));
        }

        private static Rectangle Bounding(Rectangle r0, Rectangle r1)
        {
            return new Rectangle(
                new Vec2(Math.Min(r0.Left, r1.Left), Math.Min(r0.Top, r1.Top)),
                new Vec2(Math.Max(r0.Right, r1.Right), Math.Max(r0.Bottom, r1.Bottom))
            );
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

    public class NmHpDamage : NMEvent
    {
        [UsedImplicitly] public Thing Hp;
        [UsedImplicitly] public float Amount;

        public NmHpDamage(HpArmor hp, float amount)
        {
            Hp = hp;
            Amount = amount;
        }

        public NmHpDamage()
        {
        }

        public override void Activate()
        {
            if (Hp.isServerForObject && Hp is HpArmor hp)
                hp.NetworkHit(Amount);
        }
    }
}
#endif
