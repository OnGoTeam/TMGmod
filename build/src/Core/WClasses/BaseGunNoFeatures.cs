namespace TMGmod.Core.WClasses
{
    public abstract class BaseGunNoFeatures: BaseGun
    {
        protected BaseGunNoFeatures(float xval, float yval) : base(xval, yval)
        {
        }

        protected override void OnInitialize()
        {
        }

        protected override bool DynamicAccuracy() => false;
        protected override bool DynamicKforce() => false;
        protected override bool DynamicFeatures() => false;
        public override void Fire() => RealFire();
    }
}
