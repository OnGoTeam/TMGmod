namespace TMGmod.Core.WClasses
{
    public abstract class BaseBolt:BaseGun, IAmSr
    {
        protected BaseBolt(float xval, float yval) : base(xval, yval)
        {
            BaseAccuracy = 1f;
        }

        public float BaseAccuracy { get; set; }
    }
}