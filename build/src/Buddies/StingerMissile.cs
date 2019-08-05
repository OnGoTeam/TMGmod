#if DEBUG
using DuckGame;
using System;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|DEBUG")]
    public class StingerMissile:Holdable
    {
        private Duck _target;
        public StingerMissile(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            _graphic = sprite;
            sprite.frame = 3;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
        }

        public override void Update()
        {
            UpdateTarget();
            UpdateFlight();
            base.Update();
        }

        private void UpdateTarget()
        {
            if (_target != null)
                if (Level.CheckLine<Block>(position, _target.position) != null || _target.dead || _target == owner)
                    _target = null;
            if (_target == null)
            {
                var ducks = Level.CheckCircleAll<Duck>(position, 1000);
                foreach (var d in ducks)
                {
                    if (Level.CheckLine<Block>(position, d.position) == null && d != owner && !d.dead)
                        _target = d;
                }
            }
        }

        private void UpdateFlight()
        {
            velocity += OffsetLocal(new Vec2(0.5f, 0));
            if (_target is null) return;
            var delta = _target.position - position + _target.velocity - velocity + new Vec2(0, gravity);
            var maybeangle = Math.Acos(delta.x / delta.length);
            if (delta.y < 0) maybeangle = -maybeangle;
            if (offDir < 0) maybeangle += Math.PI;
            angle = (float) maybeangle;
        }

        public override void Draw()
        {
            if (_target != null)
                Graphics.DrawCircle(_target.position, 16, Color.Red, depth: _target.depth.value + 3f);
            base.Draw();
        }
    }
}

#endif