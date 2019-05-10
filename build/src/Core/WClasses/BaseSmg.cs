using DuckGame;

namespace TMGmod.Core.WClasses
{
    /// <inheritdoc cref="BaseGun"/>
    /// <inheritdoc cref="IFirstKforce"/>
    /// <inheritdoc cref="IAmSmg"/>
    /// <summary>
    /// Base class (<see cref="BaseGun"/>) for SMGs (<see cref="IAmSmg"/>) being <see cref="IFirstKforce"/>
    /// </summary>
    public abstract class BaseSmg:BaseGun, IFirstKforce, IAmSmg
    {
        /// <summary>
        /// Kforce delay syncing
        /// </summary>
        public StateBinding DelaySmgBinding = new StateBinding(nameof(CurrDelaySmg));

        /// <inheritdoc />
        protected BaseSmg(float xval, float yval) : base(xval, yval)
        {
            KforceDSmg = 0.2f;
            MaxDelaySmg = 50;
        }

        /// <inheritdoc />
        public float KforceDSmg { get; protected set; }

        /// <inheritdoc />
        public int CurrDelaySmg { get; set; }

        /// <inheritdoc />
        public int MaxDelaySmg { get; set; }
    }
}