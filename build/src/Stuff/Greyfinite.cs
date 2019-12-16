#if DEBUG
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [BaggedProperty("CanSpawn", false)]
    [PublicAPI]
    public class Greyfinite:GreyBlock
    {
        private bool _added;

        public Greyfinite(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override void Update()
        {
            base.Update();
            if (_added || Level.CheckCircle<Duck>(position, 128) == null &&
                Level.CheckCircle<Holdable>(position, 128) == null) return;
            _added = true;
            Level.Add(new Greyfinite(x + 16, y));
        }
    }
}
#endif