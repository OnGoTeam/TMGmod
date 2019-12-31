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
            var sprite = new SpriteMap(GetPath("deleteco/Future/Stinger.png"), 42, 10);
            _graphic = sprite;
            sprite.frame = 0;
            _center = new Vec2(21f, 5f);
            _collisionOffset = new Vec2(-21f, -5f);
            _collisionSize = new Vec2(42f, 10f);
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
                velocity = OffsetLocal(new Vec2(10, 0)) + velocity - 2 * new Vec2(0f, gravity),
                owner = this,
                offDir = offDir,
                angle = angle
            };
            Level.Add(stg);
        }
    }
}
#endif