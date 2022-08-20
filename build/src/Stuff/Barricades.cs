using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [BaggedProperty("CanSpawn", false)]
    [UsedImplicitly]
    public class Barricades : GreyBlock
    {
        private bool _deployed;

        [UsedImplicitly] public EditorProperty<int> Height;

        public Barricades(float xpos, float ypos) : base(xpos, ypos)
        {
            Height = new EditorProperty<int>(2, this, 1, 4);
            _editorName = "Barricades";
        }

        public override void EditorPropertyChanged(object property)
        {
            if (Height.value > 4)
                Height.value = 4;
            if (Height.value < 1)
                Height.value = 1;
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
            for (var i = 0; i < Height.value * 4; ++i)
                Level.Add(new Barricade(vec2.x, vec2.y - i * 4));
        }

        public override void Draw()
        {
            if (Level.activeLevel is Editor)
                Graphics.DrawRect(
                    position + new Vec2(1, -8),
                    position + new Vec2(-1, -8) + new Vec2(0, -4) * Height.value * 4,
                    new Color(63, 127, 0, 127)
                );
            base.Draw();
        }
    }
}
