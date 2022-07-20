using TMGmod.Core.Modifiers.Kforce;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseSmg : BaseGun, IAmSmg
    {
        private readonly FirstKforce _firstKforce;

        protected BaseSmg(float xval, float yval) : base(xval, yval)
        {
            KforceDelta = 0.2f;
            _firstKforce = new FirstKforce(50, kforce => kforce + KforceDelta);
            Compose(_firstKforce);
        }

        protected float KforceDelta { get; set; }

        protected uint KforceDelay
        {
            set => _firstKforce.MaxDelay = value;
        }
    }
}
