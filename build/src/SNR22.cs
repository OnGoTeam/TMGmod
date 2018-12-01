using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper")]
    public class SNR22 : Gun
    {
        public SNR22 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 5;
            this._ammoType = new ATSniper();
            this._ammoType.range = 1200f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("SNR22"));
            this.center = new Vec2(14f, 6f);
            this.collisionOffset = new Vec2(-14.5f, -5f);
            this.collisionSize = new Vec2(33f, 10f);
            this._barrelOffsetTL = new Vec2(33f, 4f);
            this._fireSound = GetPath("sounds/HeavySniper.wav");
            this._fullAuto = false;
            this._fireWait = 5f;
            this._kickForce = 0.8f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.3f;
            this._holdOffset = new Vec2(0f, 1f);
            this.laserSight = true;
            this._laserOffsetTL = new Vec2(22f, 3.5f);
            this._editorName = "Gepard Lynx";
			this.weight = 6f;
        }
          public override void Update()
        {
            if (this._owner != null && this._owner.height < 17f)
            {
                this._kickForce = 0f;
				this.loseAccuracy = 0f;
                this.maxAccuracyLost = 0f;
				this.graphic = new Sprite(GetPath("SNR22bipods"));
            }
            else
            {
                this._kickForce = 0.8f;
                this.loseAccuracy = 0.1f;
                this.maxAccuracyLost = 0.3f;
				this.graphic = new Sprite(GetPath("SNR22"));
            }
            base.Update();
        }
	}
}