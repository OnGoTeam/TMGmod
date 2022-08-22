namespace TMGmod.Core
{
    public static class FrameUtils
    {
        public static int Modulo(this int value, int mod)
        {
            return (value % mod + mod) % mod;
        }
    }
}
