using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Machinegun|Custom")]
    public class AugC : Gun
    {

        private readonly SpriteMap _sprite;
        private readonly int _greathole;

        public AugC (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 425f,
                accuracy = 0.93f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("AUGA3"), 30, 12);
            graphic = _sprite;
            _greathole = Rando.Int(1, 3);
            _sprite.frame = _greathole;
            center = new Vec2(15f, 6f);
            collisionOffset = new Vec2(-15f, -6f);
            collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 5f);
            _holdOffset = new Vec2(-3f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.1f;
            _editorName = "AUG A3";
			weight = 5.5f;
        }
        public override void Draw()
        {
            _sprite.frame = _greathole;
            base.Draw();
        }
    }
}