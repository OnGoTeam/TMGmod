using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Shotgun")]
    public class DragoShot : Gun
    {
        public DragoShot (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 16;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 160f;
            this._ammoType.accuracy = 0.7f;
            this._ammoType.penetration = 2f;
            this._numBulletsPerFire = 8;
            this._ammoType.bulletThickness = 0.8f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("DragoShot"));
            this.center = new Vec2(17f, 7f);
            this.collisionOffset = new Vec2(-14f, -7f);
            this.collisionSize = new Vec2(29f, 11f);
            this._barrelOffsetTL = new Vec2(30f, 2.5f);
            this._fireSound = "shotgunFire";
            this._fullAuto = true;
            this._fireWait = 2.2f;
            this._kickForce = 1.2f;
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0.5f;
            this.laserSight = true;
            this._laserOffsetTL = new Vec2(23f, 3f);
            this._holdOffset = new Vec2(0f, 3f);
            this._editorName = "DragoShot";
			this.weight = 5f;
        }
    }
}
