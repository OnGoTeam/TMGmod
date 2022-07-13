using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    [UsedImplicitly]
    public class CandyCaneOrange : CandyCane
    {
        public CandyCaneOrange(float xval, float yval) : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("Holiday/Peppermint Orange"));
            _ammoType = new ATCaneOrange();
            _editorName = "Peppermint Orange";
        }

        public override void Drop(Vec2 pos, bool force = false, float p = 0.75f)
        {
            base.Drop(pos, false, -1f);
        }

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private class ATCaneOrange : ATCane
        {
            public ATCaneOrange()
            {
                range = 320f;
                bulletSpeed = 10f;
                sprite = new Sprite(Mod.GetPath<Core.TMGmod>("Holiday/Peppermint Orange"));
            }
        }
    }
}
