using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseAr : BaseGun, IHspeedKforce, IAmAr
    {
        protected BaseAr(float xval, float yval) : base(xval, yval)
        {
            KickForceSlowAr = 0.07f;
            KickForceFastAr = 0.8f;
        }

        public float KickForceSlowAr { get; protected set; }
        public float KickForceFastAr { get; protected set; }
    }
}
