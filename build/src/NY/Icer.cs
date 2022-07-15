using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    // ReSharper disable once InconsistentNaming
    public class Icer : BaseBolt
    {
        public Icer(float xval, float yval) : base(xval, yval)
        {
            _graphic = new SpriteMap(GetPath("Holiday/Icer"), 53, 13);
            _center = new Vec2(27f, 8f);
            _collisionOffset = new Vec2(-27f, -8f);
            _collisionSize = new Vec2(53f, 15f);
            _barrelOffsetTL = new Vec2(53f, 5f);
            _flare = new SpriteMap(GetPath("Holiday/IcerFlare"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 4;
            _ammoType = new ATIcer();
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _kickForce = 5f;
            _holdOffset = new Vec2(9f, 1f);
            _editorName = "Icer Urbana";
            _weight = 5.6f;
        }

        protected override bool HasLaser() => false;
        protected override float MaxAngle() => 0.1f;
        protected override float MaxOffset() => 4.0f;
        protected override float ReloadSpeed() => .5f;
    }
}
