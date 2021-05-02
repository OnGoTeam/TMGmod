#if DEBUG
using JetBrains.Annotations;
using DuckGame;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|DEBUG")]
    [PublicAPI]
    internal class LockedRedCase : LockedContainer<Cases.Color.PodarokColorR>
    {
        public LockedRedCase(float xval, float yval) : base(xval, yval)
        {
            _graphic = new SpriteMap(GetPath("ColoredCases"), 14, 8) { frame = 3 };
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
        }

        protected override Vec2 SpawnPos => new Vec2();
    }
}
#endif