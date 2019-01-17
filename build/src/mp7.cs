using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    //[yee] switch
    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class MP7 : Gun
    {
        public MP7(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 40;
            _ammoType = new AT9mmS
            {
                range = 190f,
                accuracy = 0.9f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("MP7"));
            center = new Vec2(12f, 4f);
            collisionOffset = new Vec2(-12f, -4f);
            collisionSize = new Vec2(20f, 10f);
            _barrelOffsetTL = new Vec2(20f, 3f);
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(3f, 1f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _editorName = "MP7";
            weight = 3f;
        }

        public override void Update()
        {
            base.Update();
            if (duck != null)
            {
                if (duck.inputProfile.Down("UP") && !_raised)
                {
                    if (offDir < 0)
                    {
                        handAngle = 0.5f;
                    }
                    else
                    {
                        handAngle = -0.5f;
                    }

                    return;
                }

                if (duck.inputProfile.Down("QUACK") && !_raised)
                {
                    if (duck.sliding) return;
                    if (offDir > 0)
                    {
                        handAngle = 0.5f;
                    }
                    else
                    {
                        handAngle = -0.5f;
                    }

                    return;
                }
            }

            handAngle = 0f;
        }

    }
}
