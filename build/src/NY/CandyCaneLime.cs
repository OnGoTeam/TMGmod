using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    [UsedImplicitly]
    public class CandyCaneLime:CandyCane
    {
        public CandyCaneLime(float xval, float yval) : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("Holiday/Peppermint Lime"));
            _ammoType = new ATCaneLime();
            _editorName = "Peppermint Lime";
        }
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private class ATCaneLime : ATCane
        {
            public ATCaneLime()
            {
                range = 650f;
                bulletSpeed = 19f;
                sprite = new Sprite(Mod.GetPath<Core.TMGmod>("Holiday/Peppermint Lime"));
            }
        }
        public override void Drop(Vec2 pos, bool force = false, float p = 0.75f)
        {
            base.Drop(pos, false, 2f);
        }
    }
}