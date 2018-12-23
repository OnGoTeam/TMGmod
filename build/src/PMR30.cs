using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Pistol")]
    // ReSharper disable once InconsistentNaming
    public class PMRC : Gun
    {

        public PMRC(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 215f,
                accuracy = 0.875f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("PMR30Civilian"));
            center = new Vec2(8f, 5f);
            collisionOffset = new Vec2(-8f, -5f);
            collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2.5f);
            _holdOffset = new Vec2(0f, 2f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.55f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.15f;
            _editorName = "PMR30";
			weight = 1f;
        }
    }
}