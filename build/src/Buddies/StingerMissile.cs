using DuckGame;
using System;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|DEBUG")]
    class StingerMissile:Holdable
    {
        Duck Target;
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
            if (Target != null)
                if (Level.CheckLine<Block>(position, Target.position) != null || Target.dead || Target == owner)
                    Target = null;
            if (Target == null)
            {
                var ducks = Level.CheckCircleAll<Duck>(position, 1000);
                foreach (var d in ducks)
                {
                    if (Level.CheckLine<Block>(position, d.position) == null && d != owner && !d.dead)
                        Target = d;
                }
            }
        }

        private void UpdateFlight()
        {
            velocity += OffsetLocal(new Vec2(0.5f, 0));
            if (Target is null) return;
            var delta = Target.position - position + Target.velocity - velocity + new Vec2(0, gravity);
            var maybeangle = Math.Acos(delta.x / delta.length);
            if (delta.y < 0) maybeangle = -maybeangle;
            if (offDir < 0) maybeangle += Math.PI;
            angle = (float) maybeangle;
        }

        public override void Draw()
        {
            if (Target != null)
                Graphics.DrawCircle(Target.position, 16, Color.Red, depth: Target.depth.value + 3f);
            base.Draw();
        }
    }
}
