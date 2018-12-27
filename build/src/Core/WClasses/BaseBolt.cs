namespace TMGmod.Core.WClasses
{
    public abstract class BaseBolt:BaseGun, ISpeedAccuracy
    {
        protected BaseBolt(float xval, float yval) : base(xval, yval)
        {
            BaseAccuracy = 1f;
            MinAccuracy = 0f;
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.5f;
        }

        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; }
    }
}