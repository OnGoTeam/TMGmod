#if DEBUG
using JetBrains.Annotations;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Buddies
{
    [PublicAPI]
    [EditorGroup("TMG|DEBUG")]
    public class StingerLauncher:BaseGun
    {
        public StingerLauncher(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            _graphic = sprite;
            sprite.frame = 4;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            ammo = 1;
        }

        public override void Fire()
        {
            var b = Offset(new Vec2(16, 0));
            var stg = new StingerMissile(b.x, b.y)
            {
                velocity = OffsetLocal(new Vec2(10, 0)) + velocity - new Vec2(0f, gravity),
                owner = this
            };
            Level.Add(stg);
        }
    }
}
#endif