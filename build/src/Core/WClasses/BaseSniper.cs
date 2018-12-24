namespace TMGmod.Core.WClasses
{
    public abstract class BaseBolt:BaseGun, IAmSr
    {
        protected BaseBolt(float xval, float yval) : base(xval, yval)
        {
        }

        public float KfState { get; set; }
    }
}