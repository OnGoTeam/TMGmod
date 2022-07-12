namespace TMGmod.Core.WClasses
{
    public abstract class BaseDmr : BaseGun, ILoseAccuracy, IAmDmr
    {
        protected BaseDmr(float xval, float yval) : base(xval, yval)
        {
            RegenAccuracyDmr = 0f;
            DrainAccuracyDmr = 0f;
        }

        public float RegenAccuracyDmr { get; protected set; }
        public float DrainAccuracyDmr { get; protected set; }
    }
}