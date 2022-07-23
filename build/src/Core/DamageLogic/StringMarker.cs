#if DEBUG
using DuckGame;

namespace TMGmod.Core.DamageLogic
{
    public class StringMarker : Thing
    {
        private readonly string _string;
        private float _alive = 1f;

        private StringMarker(Vec2 pos, string @string) : base(pos.x, pos.y)
        {
            _string = @string;
            _hSpeed = Rando.Float(-1f, 1f);
            _vSpeed = Rando.Float(-.5f, 1f);
        }

        public override void Update()
        {
            x += hSpeed;
            y += vSpeed;
            _alive -= 0.05f;
            if (_alive < 0) Level.Remove(this);
        }

        public override void Draw()
        {
            Graphics.DrawString(_string, position, new Color(Rando.Int(192, 255), Rando.Int(0, 63), 0));
        }

        public static void Show(Vec2 position, string @string)
        {
            Level.Add(new StringMarker(position, @string));
        }
    }
}
#endif
