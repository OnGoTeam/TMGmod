namespace TMGmod.Core
{
    public static class FrameUtils
    {
        public static int Modulo(this int divident, int divisor) => (divident % divisor + divisor) % divisor;

        public static int Quotient(this int divident, int divisor) => (divident - divident.Modulo(divisor)) / divisor;
    }
}
