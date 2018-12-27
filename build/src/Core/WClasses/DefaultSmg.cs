using DuckGame;

namespace TMGmod.Core.WClasses
{
    public abstract class DefaultSmg:BaseGun,IFirstKforce
    {
        public StateBinding DelaySmgBinding = new StateBinding(nameof(CurrDelaySmg));
        protected DefaultSmg(float xval, float yval) : base(xval, yval)
        {
            KforceDSmg = 0.2f;
            MaxDelaySmg = 50;
        }

        public float KforceDSmg { get; protected set; }
        public int CurrDelaySmg { get; set; }
        public int MaxDelaySmg { get; set; }
    }
}