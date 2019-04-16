using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    //[yee] switch
    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class MP7 : Gun, IAmSmg
    {
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }

        public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));

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
            _graphic = new Sprite(GetPath("MP7"));
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
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
            if (duck?.inputProfile.Down("UP") == true && !_raised)
            {
                HandAngleOff = -0.5f;

                return;
            }

            if (duck?.inputProfile.Down("QUACK") == true && !_raised && !duck.sliding)
            {
                HandAngleOff = 0.5f;

                return;
            }

            handAngle = 0f;
        }

    }
}
