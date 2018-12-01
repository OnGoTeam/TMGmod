using DuckGame;

namespace TMGmod.src
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    public class M960 : Gun
    {
		
		public EditorProperty<bool> limited = new EditorProperty<bool>(false, null, 0f, 1f, 1f, null, false, false);
		
        public M960(float xval, float yval)
            : base(xval, yval)
        {
            this.ammo = 50;
            this._ammoType = new AT9mm();
            this._ammoType.range = 185f;
            this._ammoType.accuracy = 0.9f;
            this._type = "gun";
            base.graphic = new Sprite(GetPath("M960"), 0f, 0f);
            this.center = new Vec2(13.5f, 3.5f);
            this.collisionOffset = new Vec2(-11.5f, -3.5f);
            this.collisionSize = new Vec2(23f, 7f);
            this._barrelOffsetTL = new Vec2(23f, 2.5f);
            this._fireSound = "smg";
            this._fullAuto = true;
            this._fireWait = 0.15f;
            this._kickForce = 0.3f;
            this._holdOffset = new Vec2(2.5f, 1.5f);
            this.loseAccuracy = 0.01f;
            this.maxAccuracyLost = 0.05f;
            this._editorName = "Calico M960";
			this.weight = 1.5f;
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (this.limited.value == true)
                {
                 this._fireWait = 0.5f;
                 this._ammoType.accuracy = 0.95f;
                }
            }
            base.Initialize();
        }
	}
}