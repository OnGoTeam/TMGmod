using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|LMG")]
    public class MG44 : Gun
    {
		
		public MG44 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 80;
            _ammoType = new ATMagnum
            {
                range = 750f,
                accuracy = 0.75f,
                penetration = 1.5f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("mg44req"));
            center = new Vec2(19.5f, 6f);
            collisionOffset = new Vec2(-19.5f, -6f);
            collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(40f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.6f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(4f, 0f);
            _editorName = "Magnium";
			weight = 7.5f;
        }
		public override void Update()
		{
		base.Update();
			if (ammo == 1) graphic = new Sprite(GetPath("mg44req1"), 0f, 0f);
			if (ammo == 0) graphic = new Sprite(GetPath("mg44req2"), 0f, 0f);
		}
	}
}