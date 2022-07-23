#if DEBUG
using DuckGame;

namespace TMGmod.Core.DamageLogic
{
    public class DotMarker : Thing
    {
        private int _ticks;

        private DotMarker(Vec2 position) : base(position.x, position.y)
        {
        }

        public override void Draw()
        {
            Graphics.DrawCircle(position, 2, Color.Purple);
        }

        public static void Show(Vec2 position)
        {
            Level.Add(new DotMarker(position));
        }

        public override void Update()
        {
            base.Update();
            ++_ticks;
            if (_ticks > 60) Level.Remove(this);
        }
    }
}
#endif
