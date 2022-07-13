using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseSmg : BaseGun, IFirstKforce, IAmSmg
    {
        [UsedImplicitly] public StateBinding DelaySmgBinding = new StateBinding(nameof(CurrentDelaySmg));

        protected BaseSmg(float xval, float yval) : base(xval, yval)
        {
            KickForceDeltaSmg = 0.2f;
            MaxDelaySmg = 50;
        }

        public float KickForceDeltaSmg { get; protected set; }

        public int CurrentDelaySmg { get; set; }

        public int MaxDelaySmg { get; protected set; }
    }
}
