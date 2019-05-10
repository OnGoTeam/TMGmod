using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.NY
{
    /// <inheritdoc />
    [EditorGroup("TMG|Misc|Holiday")]
    public class SnowMgun : BaseSmg
    {
        private readonly SpriteMap _sprite;

        /// <inheritdoc />
        public SnowMgun(float xval, float yval) : base(xval, yval)
        {
            ammo = 40;
            _ammoType = new ATSneg();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Holiday/SnowMachineGun"), 17, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(9.5f, 4.5f);
            _collisionOffset = new Vec2(-9.5f, -4.5f);
            _collisionSize = new Vec2(19f, 9f);
            _barrelOffsetTL = new Vec2(17f, 4.5f);
            _holdOffset = new Vec2(-2f, 1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.3f;
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.32f;
            _editorName = "SnowMacnineGun";
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
        }

        /// <inheritdoc />
        public override void Update()
        {
            if (ammo > 20 && ammo <= 30 && _sprite.frame < 1) _sprite.frame += 1;
            if (ammo > 10 && ammo <= 20 && _sprite.frame < 2) _sprite.frame += 1;
            if (ammo > 0 && ammo <= 10 && _sprite.frame < 3) _sprite.frame += 1;
            base.Update();
        }
    }
}