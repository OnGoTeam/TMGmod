#if DEBUG
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Buddies
{
    [PublicAPI]
    [EditorGroup("TMG|DEBUG")]
    public class FlameThrowerPlus : PPSh41
    {
        public FlameThrowerPlus(float xval, float yval) : base(xval, yval)
        {
            _editorName = "FTP";
        }

        public override void Fire()
        {
            var vec = Maths.AngleToVec(barrelAngle + Rando.Float(-0.2f, 0.2f));
            vec = new Vec2(vec.x * Rando.Float(6f, 8f), vec.y * Rando.Float(6f, 8f) - 1);
            Level.Add(SmallFire.New(barrelPosition.x, barrelPosition.y, vec.x, vec.y));
        }
    }
}
#endif
