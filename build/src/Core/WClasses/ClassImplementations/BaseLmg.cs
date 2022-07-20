using DuckGame;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Core.WClasses.ClassImplementations
{
    public abstract class BaseLmg : BaseGun, IAmLmg
    {
        protected BaseLmg(float xval, float yval) : base(xval, yval)
        {
            MaxAccuracy = 0.8f;
            MinAccuracy = 0.7f;
            KickForce1Lmg = 0.4f;
            KickForce2Lmg = 0.7f;
        }

        protected override float BaseKforce => Rando.Float(KickForce1Lmg, KickForce2Lmg);

        protected float KickForce1Lmg { get; set; }

        protected float KickForce2Lmg { get; set; }
    }
}
