using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|LMG")]
    public class M16LMG : Gun
    {
		
		public M16LMG (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 95;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 500f;
            this._ammoType.accuracy = 0.8f;
            this._ammoType.penetration = 1.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("m4lmg"));
            this.center = new Vec2(19f, 6f);
            this.collisionOffset = new Vec2(-19f, -6f);
            this.collisionSize = new Vec2(38f, 11f);
            this._barrelOffsetTL = new Vec2(38f, 4f);
            this._fireSound = "deepMachineGun";
            this._fullAuto = true;
            this._fireWait = 0.825f;
            this._kickForce = 0.33f;
            this.loseAccuracy = 0.01f;
            this.maxAccuracyLost = 0.12f;
            this._holdOffset = new Vec2(5f, 1f);
            this._editorName = "M16-LMG";
			this.weight = 5.75f;
        }
          public override void Update()
        {
            if (this._owner != null && this._owner.height < 17f)
            {
                this._kickForce = 0.05f;
				this.loseAccuracy = 0.005f;
                this.maxAccuracyLost = 0.6f;
            }
            else
            {
                this._kickForce = 0.33f;
                this.loseAccuracy = 0.01f;
                this.maxAccuracyLost = 0.12f;
            }
            base.Update();
        }
	}
}	