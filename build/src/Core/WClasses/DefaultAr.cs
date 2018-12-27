namespace TMGmod.Core.WClasses
{
    public abstract class DefaultAr:BaseGun,IHspeedKforce
    {
        protected DefaultAr(float xval, float yval) : base(xval, yval)
        {
            Kforce1Ar = 0.07f;
            Kforce2Ar = 0.8f;
        }

        public float Kforce1Ar { get; protected set; }
        public float Kforce2Ar { get; protected set; }
    }
}