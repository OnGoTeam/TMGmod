using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    [UsedImplicitly]
    public class CandyCaneOrange:CandyCane
    {
        public CandyCaneOrange(float xval, float yval) : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("Holiday/Peppetmint Orange"));
            _ammoType = new ATCane(_graphic)
            {
                range = 500f,
                accuracy = 0.95f
            };
            _editorName = "CandyCaneOrange";
        }

        public override void Drop(Vec2 pos, bool force = false, float p = 0.75f)
        {
            base.Drop(pos, false, -1f);
        }
    }
}