using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseBurst : BaseGun
    {
        protected int BurstNum = 2;
        protected float DeltaWait  = .1f;

        [UsedImplicitly] public StateBinding ShotsBinding = new StateBinding(nameof(ShotsLeft));

        [UsedImplicitly] public int ShotsLeft;
        [UsedImplicitly] public StateBinding BurstBinding = new StateBinding(nameof(BurstEnabled));

        [UsedImplicitly] public bool BurstEnabled = true;

        protected BaseBurst(float xval, float yval) : base(xval, yval)
        {
        }

        public override void Fire()
        {
            if (ShotsLeft > 0 || _wait > 0) return;
            base.Fire();
            if (!BurstEnabled) return;
            // else
            ShotsLeft = BurstNum - 1;
            if (ShotsLeft > 0) _wait = DeltaWait;
        }

        public override void Update()
        {
            if (isLocal && !(_wait > 0f) && ShotsLeft > 0)
            {
                base.Fire();
                ShotsLeft -= 1;
                if (ShotsLeft > 0) _wait = DeltaWait;
            }
            base.Update();
        }
    }
}
