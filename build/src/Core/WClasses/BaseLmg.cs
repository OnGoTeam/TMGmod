namespace TMGmod.Core.WClasses
{
    public abstract class BaseLmg : BaseGun, IRandKforce, IAmLmg
    {
        protected BaseLmg(float xval, float yval) : base(xval, yval)
        {
            BaseAccuracy = 0.8f;
            MinAccuracy = 0.7f;
            KickForce1Lmg = 0.4f;
            KickForce2Lmg = 0.7f;
        }

        public float KickForce1Lmg { get; protected set; }

        public float KickForce2Lmg { get; protected set; }
    }
}