using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|LMG|Custom")]
    // ReSharper disable once InconsistentNaming
    public class MG44C : BaseLmg
    {
		public MG44C (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 750f,
                accuracy = 0.75f,
                penetration = 1.5f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("mg44reqnato2"));
            center = new Vec2(19.5f, 6f);
            collisionOffset = new Vec2(-19.5f, -6f);
            collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(40f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.3f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(4f, 0f);
            _editorName = "Magnium with NATO Mag";
			weight = 6f;
            BaseAccuracy = 0.75f;
            MinAccuracy = 0.7f;
            Kforce1Lmg = 0.2f;
            Kforce2Lmg = 0.4f;
        }
    }
}