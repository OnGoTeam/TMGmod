using DuckGame;

namespace TMGmod.Core
{
    // ReSharper disable once InconsistentNaming
    public class ATpizdec : AmmoType
    {
        public ATpizdec()
        {
			bulletLength = 30f;
            combustable = true;
            bulletSpeed = 15f;
            range = 500f;
            accuracy = 1f;
            penetration = 100f;
            bulletThickness = 5f;
        }
    }
}
