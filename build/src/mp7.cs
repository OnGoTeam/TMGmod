using DuckGame;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
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
            ammo = 40;
            _ammoType = new AT9mmS {range = 190f, accuracy = 0.9f};
            _type = "gun";
            _graphic = new Sprite(GetPath("MP7"));
            _center = new Vec2(12f, 4f);
            _collisionOffset = new Vec2(-12f, -4f);
            _collisionSize = new Vec2(20f, 10f);
            _barrelOffsetTL = new Vec2(20f, 3f);
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(3f, 1f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _editorName = "MP7";
            _weight = 3f;
        }

        public override void Update()
        {
            base.Update();
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Down("UP") && !_raised && !duck.inputProfile.Down("QUACK"))
                    {
                        if (offDir < 0)
                        {
                            //this._angleOffset = 0.5f;
                            handAngle = 0.5f;
                        }
                        else
                        {
                            //this._angleOffset = -0.5f;
                            handAngle = -0.5f;
                        }

                        return;
                    }

                    else if (duck.inputProfile.Down("DOWN") && !_raised && !duck.inputProfile.Down("QUACK"))
                    {
                        if (offDir > 0)
                        {
                            //this._angleOffset = 0.5f;
                            handAngle = 0.5f;
                        }
                        else
                        {
                            //this._angleOffset = -0.5f;
                            handAngle = -0.5f;
                        }

                        return;
                    }
                }
            }

            handAngle = 0f;
        }

    }
}
