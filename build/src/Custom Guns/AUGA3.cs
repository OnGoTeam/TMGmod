using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Machinegun|Custom")]
    public class AugC : Gun
    {
        public AugC (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 680f,
                accuracy = 0.96f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("auga3"));
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
	}
}