using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for burst-firingwith custom shot count
    /// </summary>
    public abstract class BaseBurst:BaseGun
    {
        /// <summary>
        /// Burst countdown
        /// </summary>
        [UsedImplicitly]
        public int ShotsLeft;
        /// <summary>
        /// For syncing
        /// </summary>
        [UsedImplicitly]
        public StateBinding ShotsBinding = new StateBinding(nameof(ShotsLeft));
        /// <summary>
        /// Time between two burst shots
        /// </summary>
        protected float DeltaWait;
        /// <summary>
        /// Burst length (in shots)
        /// </summary>
        protected int BurstNum;

        /// <inheritdoc />
        protected BaseBurst(float xval, float yval) : base(xval, yval)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Starts burst
        /// </summary>
        public override void Fire()
        {
            if (ShotsLeft > 0 || _wait > 0) return;
            base.Fire();
            ShotsLeft = BurstNum - 1;
            if (ShotsLeft > 0) _wait = DeltaWait;
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates burst state
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (_wait > 0f || ShotsLeft <= 0) return;
            base.Fire();
            ShotsLeft -= 1;
            if (ShotsLeft > 0) _wait = DeltaWait;
        }
    }
}