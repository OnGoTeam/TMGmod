#if DEBUG
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|DEBUG")]
    [BaggedProperty("canSpawn", false)]
    [UsedImplicitly]
    public class PiercingBazooka : Bazooka
    {
        public PiercingBazooka(float xval, float yval) : base(xval, yval)
        {
            _ammoType = new AtPiercingMissile();
        }
    }

    public class AtPiercingMissile : ATMissile
    {
        public AtPiercingMissile()
        {
            bulletType = typeof(AtpmBullet);
            affectedByGravity = true;
            gravityMultiplier = 0;
            range = 400f;
        }
    }

    public class AtpmBullet : Bullet
    {
        private readonly Vec2 _realEnd;
        private float _frameTimer;

        public AtpmBullet(
            float xval, float yval, AmmoType type, float ang = -1, Thing owner = null,
            bool rbound = false, float distance = -1, bool tracer = false, bool network = true
        ) : base(
            xval, yval, type,
            ang, owner, rbound, distance, tracer, network
        )
        {
            _realEnd = end;
        }

        private void AddBullet(Bullet bullet)
        {
            bullet.lastReboundSource = lastReboundSource;
            bullet.isLocal = isLocal;
            Level.Add(bullet);
        }

        private float MissileRange()
        {
            return _totalLength - (_actualStart - start).length;
        }

        private float MissileAngle()
        {
            return -Maths.PointDirection(Vec2.Zero, travelDirNormalized);
        }

        private Vec2 MissilePosition()
        {
            return start + travelDirNormalized * 32;
        }

        private Bullet MakeMissileAt(Vec2 pos)
        {
            return new ATMissile().GetBullet(
                pos.x, pos.y, angle: MissileAngle(), firedFrom: firedFrom, distance: MissileRange(), tracer: _tracer
            );
        }

        private Bullet MakeMissile()
        {
            return MakeMissileAt(MissilePosition());
        }

        private void AddMissile()
        {
            AddBullet(MakeMissile());
        }

        protected override void OnHit(bool destroyed)
        {
            if (destroyed)
                AddMissile();
            else
                base.OnHit(false);
        }

        private void UpdateVelocityPerDeltaNormalized(Vec2 deltaNormalized)
        {
            var vec = deltaNormalized.Rotate(Maths.PI / 2, Vec2.Zero);
            var coeff = Maths.FastSin(20f * _frameTimer) * 0.1f;
            velocity = _bulletSpeed * (deltaNormalized + vec * coeff);
        }

        private void UpdateTimer()
        {
            _frameTimer += 1f / 60f;
        }

        private Vec2 Delta()
        {
            return _realEnd - start;
        }

        private void UpdateVelocityPerDelta(Vec2 delta)
        {
            if (!(delta.length > 16f)) return;
            // else
            UpdateVelocityPerDeltaNormalized(delta.normalized);
        }

        private void UpdateVelocity()
        {
            UpdateVelocityPerDelta(Delta());
        }

        private void UpdateTimerAndVelocity()
        {
            UpdateTimer();
            UpdateVelocity();
        }

        public override void Update()
        {
            UpdateTimerAndVelocity();
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
#if DEBUG
            Graphics.DrawCircle(_realEnd, 16f, Color.Red);
#endif
        }
    }
}
#endif
