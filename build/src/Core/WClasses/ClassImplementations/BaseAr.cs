using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Core.WClasses.ClassImplementations
{
    public abstract class BaseAr : BaseGun, IAmAr
    {
        protected BaseAr(float xval, float yval) : base(xval, yval)
        {
            Compose(new HSpeedKforce(this, hspeed => hspeed > KforceSpeedThreshold, kforce => kforce + KforceDelta));
        }

        protected float KforceDelta { get; set; } = .73f;
        private const float KforceSpeedThreshold = .1f;
    }
}
