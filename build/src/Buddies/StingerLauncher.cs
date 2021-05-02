#if DEBUG
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;

namespace TMGmod.Buddies
{
    [PublicAPI]
    [EditorGroup("TMG|Explosive|Rocketlauncher")]
    public class StingerLauncher : BaseGun
    {
        public StingerLauncher(float xval, float yval) : base(xval, yval)
        {
            _graphic = new SpriteMap(GetPath("deleteco/Future/Stinger.png"), 42, 10) { frame = 0 };
            _center = new Vec2(21f, 5f);
            _collisionOffset = new Vec2(-21f, -5f);
            _collisionSize = new Vec2(42f, 10f);
            _holdOffset = new Vec2(-12f, 0f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            ammo = 1;
            _kickForce = 7f;
        }

        public override void Fire()
        {
            if (ammo <= 0) return;
            // else
            var pos = Offset(new Vec2(32, 0));
            var stg = new StingerMissile(pos.x, pos.y)
            {
                velocity = OffsetLocal(new Vec2(10, 0)) + velocity - 2 * new Vec2(0f, gravity),
                owner = this,
                offDir = offDir,
                angle = angle
            };
            Level.Add(stg);
            ApplyKick();
            --ammo;
        }
    }
}
#endif