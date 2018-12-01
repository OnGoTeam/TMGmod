using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|AutoPistol")]
    public class Glock18 : Gun
    {
		
        public Glock18(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 17;
            this._ammoType = new AT9mm();
            this._ammoType.range = 100f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Glock17"));
            this.center = new Vec2(8f, 3f);
            this.collisionOffset = new Vec2(-7.5f, -3.5f);
            this.collisionSize = new Vec2(16f, 11f);
            this._barrelOffsetTL = new Vec2(16f, 1f);
            this._fireSound = GetPath("sounds/2.wav");
            this._fullAuto = true;
            this._fireWait = 0.4f;
            this._kickForce = 0.1f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.2f;
            this._editorName = "Glock 18";
			this.weight = 2f;
        }
	}
}