using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATIcer : BaseAmmoType
    {
        public ATIcer()
        {
            var spriteY = new Sprite(Mod.GetPath<Core.TMGmod>("Holiday/Icer Bullet"));
            spriteY.CenterOrigin();
            bulletType = typeof(IcerBullet);
            sprite = spriteY;
            penetration = 0f;
            accuracy = 1f;
            bulletSpeed = 10f;
            range = 1000f;
            bulletLength = 5f;
            bulletThickness = 3f;
            immediatelyDeadly = false;
        }

        private class IcerBullet : Bullet
        {
            public IcerBullet(
                float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false,
                float distance = -1, bool tracer = false, bool network = true
            ) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
            {
            }

            public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
            {
                base.OnCollide(pos, t, willBeStopped);
                if (!willBeStopped) return;
                if (!(t is MaterialThing mt)) return;
                var pos0 = pos - travelDirNormalized * 10;
                Level.Add(Icicle.FromStick(pos0, ammo.sprite.angle, travelDirNormalized * bulletSpeed, mt));
            }
        }

        private class Icicle : PhysicsObject, IPlatform
        {
            private MaterialThing _stick;

            [UsedImplicitly] public StateBinding OdoBinding = new StateBinding(nameof(OffDirOffset));

            [UsedImplicitly] public sbyte OffDirOffset = 1;

            [UsedImplicitly] public StateBinding SaoBinding = new StateBinding(nameof(StickAngleOffset));

            [UsedImplicitly] public StateBinding SoBinding = new StateBinding(nameof(StickOffset));

            [UsedImplicitly] public float StickAngleOffset;

            [UsedImplicitly] public Vec2 StickOffset;

            public Icicle(float xval, float yval) : base(xval, yval)
            {
                _graphic = new Sprite(Mod.GetPath<Core.TMGmod>("Holiday/Icer Bullet"));
                _graphic.CenterOrigin();
                _weight = 1f;
                _collisionSize = new Vec2(12f, 5f);
                _collisionOffset = new Vec2(-6f, -3f);
            }

            internal static Icicle FromStick(Vec2 pos, float angle, Vec2 vel, MaterialThing stick)
            {
                var i = new Icicle(pos.x, pos.y) { _stick = stick, _angle = angle, velocity = vel };
                i.InitiateStick();
                return i;
            }

            public override void Touch(MaterialThing with)
            {
                base.Touch(with);
                UpdateStickable();
                if (!(_stick is null)) return;
                if (with is Duck d)
                {
                    d.Kill(new DTImpact(this));
                    return;
                }

                _stick = with;
                InitiateStick();
            }

            private void UpdateStickable()
            {
                if (_stick == null) return;
                if (_stick._destroyed)
                {
                    _stick = null;
                    return;
                }

                if (_stick is FeatherVolume || _stick is Duck) _stick = null;
            }

            public override void Update()
            {
                UpdateStick();
                base.Update();
                UpdateStick();
            }

            private void UpdateStick()
            {
                UpdateStickable();
                if (_stick == null) return;
                if (_stick.destroyed) return;
                angle = _stick.angle + StickAngleOffset * OffDirOffset * _stick.offDir;
                position = _stick.Offset(StickOffset);
                offDir = (sbyte)(OffDirOffset * _stick.offDir);
                velocity = _stick.velocity;
                _grounded = _stick.grounded || _stick is IPlatform || velocity.length < 0.01f;
            }

            private void InitiateStick()
            {
                if (_stick == null) return;
                if (_stick.destroyed) return;
                StickAngleOffset = angle - _stick.angle;
                StickOffset = _stick.ReverseOffset(position);
                StickOffset.x *= _stick.offDir;
                OffDirOffset = (sbyte)(offDir * _stick.offDir);
            }
        }
    }
}
