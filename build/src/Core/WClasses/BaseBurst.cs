namespace TMGmod.Core.WClasses
{
    public abstract class BaseBurst:BaseGun
    {
        private int _shotsLeft;
        protected float DeltaWait;
        protected int BurstNum;
        protected BaseBurst(float xval, float yval) : base(xval, yval)
        {
            ToPrevKforce = true;
        }

        public override void Fire()
        {
            if (_shotsLeft > 0) return;
            base.Fire();
            _shotsLeft = BurstNum - 1;
            if (_shotsLeft > 0) _wait = DeltaWait;
        }

        public override void Update()
        {
            base.Update();
            if (_wait > 0f || _shotsLeft <= 0) return;
            base.Fire();
            _shotsLeft -= 1;
            if (_shotsLeft > 0) _wait = DeltaWait;
        }
    }
}