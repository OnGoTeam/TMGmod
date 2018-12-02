using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Rifle")]
    // ReSharper disable once InconsistentNaming
    public class HK417 : Gun
    {
        public HK417 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 10;
            _ammoType = new ATMagnum
            {
                range = 600f,
                accuracy = 0.9f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("Hk417"));
            center = new Vec2(15f, 5f);
            collisionOffset = new Vec2(-14.5f, -5f);
            collisionSize = new Vec2(30f, 10f);
            _barrelOffsetTL = new Vec2(31f, 3f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = false;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "Hk-417C";
			weight = 3.5f;
        }
    }
}