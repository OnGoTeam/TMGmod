namespace TMGmod.Core.WClasses
{
    /// <inheritdoc cref="BaseGun"/>
    /// <inheritdoc cref="IRandKforce"/>
    /// <inheritdoc cref="IAmLmg"/>
    /// <summary>
    /// Base class (<see cref="BaseGun"/>) for LMGs (<see cref="IAmLmg"/>) being <see cref="IRandKforce"/>
    /// </summary>
    public abstract class BaseLmg:BaseGun, IRandKforce, IAmLmg
    {
        /// <inheritdoc />
        protected BaseLmg(float xval, float yval) : base(xval, yval)
        {
            BaseAccuracy = 0.8f;
            MinAccuracy = 0.7f;
            Kforce1Lmg = 0.4f;
            Kforce2Lmg = 0.7f;
        }

        /// <inheritdoc />
        public float Kforce1Lmg { get; protected set; }

        /// <inheritdoc />
        public float Kforce2Lmg { get; protected set; }
    }
}