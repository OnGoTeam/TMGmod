using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Rifle")]
    public class Mk20 : Gun
    {
        public Mk20 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new ATMagnum
            {
                range = 800f,
                accuracy = 0.87f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("SCARMk1"));
            center = new Vec2(16f, 7f);
            collisionOffset = new Vec2(-16.5f, -7f);
            collisionSize = new Vec2(33f, 14f);
            _barrelOffsetTL = new Vec2(33f, 5.5f);
            _holdOffset = new Vec2(1f, -1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.95f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "SCAR Mk20";
			weight = 6.5f;
        }
    }
}