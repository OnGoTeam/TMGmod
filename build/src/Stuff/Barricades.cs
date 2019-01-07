using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [PublicAPI]
    public class Barricades:GreyBlock
    {
        public Barricades(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override void Initialize()
        {
            Deploy(position + new Vec2(0f, -10f));
            base.Initialize();
        }

        private static void Deploy(Vec2 vec2)
        {
            for (var i = 0; i < 8; ++i)
            {
                var barricade = new Barricade(vec2.x, vec2.y - i * 4);
                Level.Add(barricade);
            }
        }
    }
}