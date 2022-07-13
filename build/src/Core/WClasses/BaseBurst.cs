using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseBurst : BaseGun
    {
        protected int BurstNum;
        protected float DeltaWait;

        [UsedImplicitly] public StateBinding ShotsBinding = new StateBinding(nameof(ShotsLeft));

        [UsedImplicitly] public int ShotsLeft;

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
