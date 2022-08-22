namespace TMGmod.Core
{
    public static class FrameUtils
    {
        public static int Modulo(this int value, int mod) => (value % mod + mod) % mod;
    }
}
