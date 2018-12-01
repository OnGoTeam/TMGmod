using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|SMG")]
    public class MPA27 : Gun
    {
        public MPA27(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
            this._ammoType = new AT9mm();
            this._ammoType.range = 200f;
            this._ammoType.accuracy = 0.75f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("MPA27"));
            this.center = new Vec2(6f, 7f);
            this.collisionOffset = new Vec2(-8f, -7f);
            this.collisionSize = new Vec2(16f, 14f);
            this._barrelOffsetTL = new Vec2(16f, 3f);
            this._fireSound = GetPath("sounds/2.wav");
            this._fullAuto = true;
            this._fireWait = 0.36f;
            this._kickForce = 0f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.2f;
            this._holdOffset = new Vec2(-1f, 3f);
            this._editorName = "Vista";
			this.weight = 2f;
        }


    }
}
