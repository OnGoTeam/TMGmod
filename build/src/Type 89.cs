using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Machinegun")]
    public class Type89 : BaseAr
    {
		
		public Type89(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 26;
            _ammoType = new AT9mm
            {
                range = 265f,
                accuracy = 0.91f,
                penetration = 1.5f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("Type 89"));
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(31f, 5f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.71f;
            _kickForce = 1.2f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.4f;
            _holdOffset = new Vec2(1f, 0f);
            _editorName = "Type 89";
			_weight = 4.6f;
        }
    }
}