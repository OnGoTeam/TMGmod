using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [BaggedProperty("CanSpawn", false)]
    [UsedImplicitly]
    public class Barricades:GreyBlock
    {
        private bool _deployed;

        public Barricades(float xpos, float ypos) : base(xpos, ypos)
        {
            _editorName = "Barricades";
        }

        public override void Update()
        {
            if (!_deployed) Deploy(position + new Vec2(0f, -10f));
            _deployed = true;
            base.Update();
        }

        private void Deploy(Vec2 vec2)
        {
            if (!isServerForObject) return;
            if (Level.activeLevel is Editor) return;
            for (var i = 0; i < 8; ++i)
            {
                var barricade = new Barricade(vec2.x, vec2.y - i * 4);
                Level.Add(barricade);
            }
        }

        public override void Draw()
        {
            if (Level.activeLevel is Editor)
                Graphics.DrawRect(position + new Vec2(1, -8), position + new Vec2(-1,-40),new Color(63, 127, 0, 127));
            base.Draw();
        }
    }
}