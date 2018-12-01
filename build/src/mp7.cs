using DuckGame;

namespace TMGmod.src
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    public class MP7 : Gun
    {
        /*public float _angleOffset;

        public override float angle
        {
            get
            {
                return base.angle + this._angleOffset;
            }
            set
            {
                this._angle = value;
            }
        }*/

        public MP7(float xval, float yval)
            : base(xval, yval)
        {
            this.ammo = 40;
            this._ammoType = new AT9mmS();
            this._ammoType.range = 190f;
            this._ammoType.accuracy = 0.9f;
            this._type = "gun";
            base.graphic = new Sprite(GetPath("MP7"), 0f, 0f);
            this.center = new Vec2(12f, 4f);
            this.collisionOffset = new Vec2(-12f, -4f);
            this.collisionSize = new Vec2(20f, 10f);
            this._barrelOffsetTL = new Vec2(20f, 3f);
            this._fireSound = GetPath("sounds/smg.wav");
            this._fullAuto = true;
            this._fireWait = 0.5f;
            this._kickForce = 0.5f;
            this._holdOffset = new Vec2(3f, 1f);
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.5f;
            this._editorName = "MP7";
            this.weight = 3f;
        }

        public override void Update()
        {
            base.Update();
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Down("UP") && !base._raised && !base.duck.inputProfile.Down("QUACK"))
                    {
                        if (this.offDir < 0)
                        {
                            //this._angleOffset = 0.5f;
                            this.handAngle = 0.5f;
                        }
                        else
                        {
                            //this._angleOffset = -0.5f;
                            this.handAngle = -0.5f;
                        }

                        return;
                    }

                    else if (base.duck.inputProfile.Down("DOWN") && !base._raised && !base.duck.inputProfile.Down("QUACK"))
                    {
                        if (this.offDir > 0)
                        {
                            //this._angleOffset = 0.5f;
                            this.handAngle = 0.5f;
                        }
                        else
                        {
                            //this._angleOffset = -0.5f;
                            this.handAngle = -0.5f;
                        }

                        return;
                    }
                }
            }

            this.handAngle = 0f;
        }

    }
}
