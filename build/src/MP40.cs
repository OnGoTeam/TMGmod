using DuckGame;

namespace TMGmod.src
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    public class MP40 : Gun
    {

        public MP40(float xval, float yval)
            : base(xval, yval)
        {
            this.ammo = 32;
            this._ammoType = new AT9mm();
            this._ammoType.range = 190f;
            this._ammoType.accuracy = 0.9f;
            this._type = "gun";
            base.graphic = new Sprite(GetPath("MP40WW2"), 0f, 0f);
            this.center = new Vec2(12f, 5f);
            this.collisionOffset = new Vec2(-11f, -4f);
            this.collisionSize = new Vec2(22f, 14f);
            this._barrelOffsetTL = new Vec2(25f, 2f);
            this._fireSound = "deepMachineGun";
            this._fullAuto = true;
            this._fireWait = 0.5f;
            this._kickForce = 0.5f;
            this._holdOffset = new Vec2(3f, 1f);
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.5f;
            this._editorName = "MP40";
			this.weight = 3f;
        }


    }
}
