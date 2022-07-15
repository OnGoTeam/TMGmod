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

        private Thing MakeFire(Vec2 fireVelocity)
        {
            return SmallFire.New(barrelPosition.x, barrelPosition.y, fireVelocity.x, fireVelocity.y);
        }

        private Vec2 FireVelocity(Vec2 fireDirection)
        {
            return new Vec2(fireDirection.x * Rando.Float(6f, 8f), fireDirection.y * Rando.Float(6f, 8f) - 1);
        }

        private Vec2 FireDirection()
        {
            return Maths.AngleToVec(barrelAngle + Rando.Float(-0.2f, 0.2f));
        }

        private Vec2 FireVelocity()
        {
            return FireVelocity(FireDirection());
        }

        private Thing MakeFire()
        {
            return MakeFire(FireVelocity());
        }

        public override void Fire()
        {
            Level.Add(MakeFire());
        }
    }
}
#endif
