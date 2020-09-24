using DuckGame;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseDmr : BaseGun, ILoseAccuracy, IAmDmr
    {
        protected BaseDmr(float xval, float yval) : base(xval, yval)
        {
            RhoAccuracyDmr = 0f;
            DeltaAccuracyDmr = 0f;
        }

        public float RhoAccuracyDmr { get; protected set; }
        public float DeltaAccuracyDmr { get; protected set; }

        public override void Draw()
        {
            base.Draw();
            var a = (1 - ammoType.accuracy) / 2;
            var v = OffsetLocal(new Vec2(64, 0));
            Graphics.DrawLine(barrelPosition, barrelPosition + v.Rotate(a, Vec2.Zero), Color.Red);
            Graphics.DrawLine(barrelPosition, barrelPosition + v.Rotate(-a, Vec2.Zero), Color.Red);
        }
    }
}