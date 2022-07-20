using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Core.WClasses.ClassImplementations
{
    public abstract class BaseDmr : BaseGun, IAmDmr
    {
        private readonly LoseAccuracy _loseAccuracy;

        protected BaseDmr(float xval, float yval) : base(xval, yval)
        {
            _loseAccuracy = new LoseAccuracy(0f, 0f, 1f);
            Compose(_loseAccuracy);
        }

        protected float RegenAccuracyDmr
        {
            set => _loseAccuracy.Regen = value;
        }

        protected float DrainAccuracyDmr
        {
            set => _loseAccuracy.Drain = value;
        }

        protected float MaxDrain
        {
            set => _loseAccuracy.Max = value;
        }
    }
}
