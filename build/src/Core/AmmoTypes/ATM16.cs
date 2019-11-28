#if DEBUG
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// <see cref="AmmoType"/> for <see cref="X3X"/>
    /// </summary>
    public class ATM16 : AmmoType
    {
        /// <inheritdoc />
        public ATM16()
        {
            bulletLength = 30f;
            combustable = true;
            bulletSpeed = 110f;
            range = 450f;
            accuracy = 0.91f;
            penetration = 2f;
            bulletThickness = 1.5f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var difficultToSee = new X3XShell(x, y) {hSpeed = dir * (5f + Rando.Float(1f))}; //должна быть кастомная shell
            Level.Add(difficultToSee);
        }
    }
}
#endif