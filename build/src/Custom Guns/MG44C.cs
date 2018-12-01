using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|LMG|Custom")]
    public class MG44C : Gun
    {
		
		
		public MG44C (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 750f;
            this._ammoType.accuracy = 0.75f;
            this._ammoType.penetration = 1.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("mg44reqnato2"));
            this.center = new Vec2(19.5f, 6f);
            this.collisionOffset = new Vec2(-19.5f, -6f);
            this.collisionSize = new Vec2(39f, 12f);
            this._barrelOffsetTL = new Vec2(40f, 4f);
            this._fireSound = "deepMachineGun";
            this._fullAuto = true;
            this._fireWait = 0.9f;
            this._kickForce = 0.3f;
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0f;
            this._holdOffset = new Vec2(4f, 0f);
            this._editorName = "Magnium with NATO Mag";
			this.weight = 6f;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
	}
}