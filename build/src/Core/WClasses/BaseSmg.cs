using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseSmg:BaseGun, IFirstKforce, IAmSmg
    {
        [UsedImplicitly]
        public StateBinding DelaySmgBinding = new StateBinding(nameof(CurrDelaySmg));

        protected BaseSmg(float xval, float yval) : base(xval, yval)
        {
            KforceDSmg = 0.2f;
            MaxDelaySmg = 50;
        }

        public float KforceDSmg { get; protected set; }

        public int CurrDelaySmg { get; set; }

        public int MaxDelaySmg { get; protected set; }
    }
}