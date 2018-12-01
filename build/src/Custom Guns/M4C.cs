using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun|Custom")]
    public class M4A1C : Gun
    {
		
		public M4A1C (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
            this._ammoType = new AT9mm();
            this._ammoType.range = 300f;
            this._ammoType.accuracy = 0.91f;
            this._ammoType.penetration = 1.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("m4a1Custm"));
            this.center = new Vec2(15f, 6f);
            this.collisionOffset = new Vec2(-15f, -6f);
            this.collisionSize = new Vec2(30f, 12f);
            this._barrelOffsetTL = new Vec2(31f, 5f);
            this._fireSound = "deepMachineGun";
            this._fullAuto = true;
            this._fireWait = 0.65f;
            this._kickForce = 0f;
            this.loseAccuracy = 0.01f;
            this.maxAccuracyLost = 0.09f;
            this._holdOffset = new Vec2(2f, 0f);
            this._editorName = "M4A1 Custom";
			this.weight = 4f;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
	}
}