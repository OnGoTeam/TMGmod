namespace TMGmod.Core.WClasses
{
    public abstract class DefaultLmg:BaseGun,IRandKforce
    {
        protected DefaultLmg(float xval, float yval) : base(xval, yval)
        {
            BaseAccuracy = 0.8f;
            MinAccuracy = 0.7f;
            Kforce1Lmg = 0.4f;
            Kforce2Lmg = 0.7f;
        }

        public float Kforce1Lmg { get; protected set; }
        public float Kforce2Lmg { get; protected set; }
    }
}