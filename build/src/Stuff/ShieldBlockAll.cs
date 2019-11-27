using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    /// <inheritdoc />
    /// <summary>
    /// Thing denoting whether shield should block all things
    /// </summary>
    [UsedImplicitly]
    [EditorGroup("TMG|Misc")]
    public class ShieldBlockAll: Thing
    {
        /// <inheritdoc />
        public ShieldBlockAll()
        {
            _graphic = new Sprite("swirl");
            _center = new Vec2(8f, 8f);
            _collisionSize = new Vec2(16f, 16f);
            _collisionOffset = new Vec2(-8f, -8f);
            _canFlip = false;
            _visibleInGame = false;
        }
    }
}