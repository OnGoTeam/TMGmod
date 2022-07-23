#if DEBUG

using DuckGame;

namespace TMGmod.Core.DamageLogic
{
    public class StrokeMarker : Thing
    {
        private int _ticks;
        private readonly Vec2 _pos2;

        private StrokeMarker(Vec2 position, Vec2 pos2) : base(position.x, position.y)
        {
            _pos2 = pos2;
        }

        public override void Draw()
        {
            Graphics.DrawLine(position, _pos2, Color.Gold);
        }

        public static void Show(Vec2 position, Vec2 pos2)
        {
            Level.Add(new StrokeMarker(position, pos2));
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
