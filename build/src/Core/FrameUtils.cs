using DuckGame;

namespace TMGmod.Core
{
    public static class FrameUtils
    {
        public static int Modulo(this int divident, int divisor) => (divident % divisor + divisor) % divisor;
#if DEBUG
        public static int Quotient(this int divident, int divisor) => (divident - divident.Modulo(divisor)) / divisor;
#endif
        public static SpriteMap TakeZis() => new(Mod.GetPath<TMGmod>("takezis"), 4, 4);

        public static SpriteMap FlareSilencer() =>
            new(Mod.GetPath<TMGmod>("FlareSilencer"), 13, 10) { center = new Vec2(0.0f, 5f) };

        public static SpriteMap FlareOnePixel0() =>
            new(Mod.GetPath<TMGmod>("FlareOnePixel0"), 13, 10) { center = new Vec2(0f, 5.5f) };

        public static SpriteMap FlareOnePixel1() =>
            new(Mod.GetPath<TMGmod>("FlareOnePixel1"), 13, 10) { center = new Vec2(0f, 5.5f) };

        public static SpriteMap FlareOnePixel2() =>
            new(Mod.GetPath<TMGmod>("FlareOnePixel2"), 13, 10) { center = new Vec2(0f, 5.5f) };

        public static SpriteMap FlareOnePixel3() =>
            new(Mod.GetPath<TMGmod>("FlareOnePixel3"), 13, 10) { center = new Vec2(0f, 5.5f) };

        public static SpriteMap FlareOnePixel4() =>
            new(Mod.GetPath<TMGmod>("FlareOnePixel4"), 13, 10) { center = new Vec2(0f, 5.5f) };

        public static SpriteMap FlareOnePixel5() =>
            new(Mod.GetPath<TMGmod>("FlareOnePixel5"), 13, 10) { center = new Vec2(1f, 5.5f) };

        public static SpriteMap FlareOnePixel3Silenced() =>
            new(Mod.GetPath<TMGmod>("FlareOnePixel3Silenced"), 13, 10) { center = new Vec2(0f, 5.5f) };

        public static SpriteMap SmallFlare() => new("smallFlare", 11, 10) { center = new Vec2(0.0f, 5f) };

        public static SpriteMap FlareBase2() => new(Mod.GetPath<TMGmod>("FlareBase2"), 13, 10) { center = new Vec2(0f, 5f) };

        public static SpriteMap FlareBase3() => new(Mod.GetPath<TMGmod>("FlareBase3"), 13, 10) { center = new Vec2(0f, 5f) };

        public static void SwitchedSilencer(bool previous)
        {
            SFX.Play(
                previous
                    ? Mod.GetPath<TMGmod>("sounds/silencer_off.wav")
                    : Mod.GetPath<TMGmod>("sounds/silencer_on.wav")
            );
        }
    }
}
