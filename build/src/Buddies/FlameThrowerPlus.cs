#if DEBUG
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Buddies
{
    [PublicAPI]
    [EditorGroup("TMG|DEBUG")]
    public class FlameThrowerPlus: PPSh41
    {
        public FlameThrowerPlus(float xval, float yval) : base(xval, yval)
        {
            _editorName = "FTP";
        }

        public override void Fire()
        {
            var vec = Maths.AngleToVec(barrelAngle + Rando.Float(-0.2f, 0.2f));
            var vec2 = new Vec2(vec.x * Rando.Float(6f, 8f), vec.y * Rando.Float(6f, 8f) - 1);
            var fire = SmallFire.New(barrelPosition.x, barrelPosition.y, vec2.x, vec2.y);
            Level.Add(fire);
        }
    }
}
#endif