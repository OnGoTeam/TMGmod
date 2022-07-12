using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseBurst : BaseGun
    {
        [UsedImplicitly]
        public int ShotsLeft;
        [UsedImplicitly]
        public StateBinding ShotsBinding = new StateBinding(nameof(ShotsLeft));
        protected float DeltaWait;
        protected int BurstNum;

        protected BaseBurst(float xval, float yval) : base(xval, yval)
        {
        }

        public override void Fire()
        {
            if (ShotsLeft > 0 || _wait > 0) return;
            base.Fire();
            ShotsLeft = BurstNum - 1;
            if (ShotsLeft > 0) _wait = DeltaWait;
        }

        public override void Update()
        {
            base.Update();
            if (!isLocal || _wait > 0f || ShotsLeft <= 0) return;
            base.Fire();
            ShotsLeft -= 1;
            if (ShotsLeft > 0) _wait = DeltaWait;
        }
    }
}