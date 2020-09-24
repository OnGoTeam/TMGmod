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
    }
}