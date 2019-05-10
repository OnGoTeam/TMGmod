namespace TMGmod.Core.WClasses
{
    /// <inheritdoc cref="BaseGun"/>
    /// <inheritdoc cref="IHspeedKforce"/>
    /// <inheritdoc cref="IAmAr"/>
    /// <summary>
    /// Base class (<see cref="BaseGun"/>) for ARs (<see cref="IAmAr"/>) being <see cref="IHspeedKforce"/>
    /// </summary>
    public abstract class BaseAr:BaseGun, IHspeedKforce, IAmAr
    {
        /// <inheritdoc />
        protected BaseAr(float xval, float yval) : base(xval, yval)
        {
            Kforce1Ar = 0.07f;
            Kforce2Ar = 0.8f;
        }

        /// <inheritdoc />
        public float Kforce1Ar { get; protected set; }

        /// <inheritdoc />
        public float Kforce2Ar { get; protected set; }
    }
}