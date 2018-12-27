using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MAP : DefaultSmg
    {
        public MAP(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 27;
            _ammoType = new AT9mm
            {
                range = 100f,
                accuracy = 0.4f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("Mixaly4sPistol"));
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-8f, -7f);
            collisionSize = new Vec2(16f, 15f);
            _barrelOffsetTL = new Vec2(16f, 5f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 0f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _holdOffset = new Vec2(-2f, 2f);
            _editorName = "Michael";
			weight = 2.5f;
        }
    }
}
