using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Rifle")]
    public class Arx200 : Gun
    {
        public Arx200 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new ATMagnum
            {
                range = 750f,
                accuracy = 0.95f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("ARX200"));
            center = new Vec2(16f, 7f);
            collisionOffset = new Vec2(-16.5f, -7f);
            collisionSize = new Vec2(33f, 14f);
            _barrelOffsetTL = new Vec2(33f, 5.5f);
            _holdOffset = new Vec2(1f, -1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.65f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.15f;
            _editorName = "Beretta ARX-200";
			weight = 5.75f;
        }
    }
}