using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Machinegun|Custom")]
    public class M4A1C : DefaultAr
    {
		
		public M4A1C (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 300f,
                accuracy = 0.91f,
                penetration = 1.5f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("m4a1Custm"));
            center = new Vec2(15f, 6f);
            collisionOffset = new Vec2(-15f, -6f);
            collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(31f, 5f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.65f;
            _kickForce = 0f;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.09f;
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "M4A1 Custom";
			weight = 4f;
        }
    }
}