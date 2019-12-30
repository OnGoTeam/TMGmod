using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    /// <inheritdoc />
    public class ATPopcorn : BaseAmmoTypeT
    {
        private readonly SpriteMap _sprite;
        /// <inheritdoc />
        public ATPopcorn()
        {
            bulletType = typeof(PopBullet);
            _sprite = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/Popcal"), 3, 3);
            _sprite.frame = Rando.Int(0, 8);
            sprite = _sprite;
            bulletSpeed = 8.5f;
            range = 400f;
            accuracy = 0.85f;
            bulletLength = 3f;
            penetration = 0.7f;
            affectedByGravity = true;
            weight = 0f;
            barrelAngleDegrees = Rando.Float(11f, -11f);
        }
        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            barrelAngleDegrees = Rando.Float(6f, -18f);
            _sprite.frame = Rando.Int(0, 8);
            sprite = _sprite;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
