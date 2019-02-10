using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;


namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    [PublicAPI]
    public class Vixr : Gun
    {
		public bool Stockngrip;
        public StateBinding StockBinding = new StateBinding(nameof(Stockngrip));

        public Vixr(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 25;
            _ammoType = new AT9mmS
            {
               range = 300f,
               accuracy = 0.88f,
               penetration = 1f,
               bulletSpeed = 21f
            };
            _type = "gun";
			//I'M BLUE DARUDE SANDSTORM DA DUBAI
            _graphic = new Sprite(GetPath("VixrStock"));
            _center = new Vec2(16.5f, 4.5f);
            _collisionOffset = new Vec2(-16.5f, -4.5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(34f, 3.5f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.65f;
            _kickForce = 0.8f;
            loseAccuracy = 0.099f;
            maxAccuracyLost = 0.17f;
            _editorName = "VIXR";
			_weight = 3.9f;
            handAngle = 0f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Stockngrip)
                {
                    graphic = new Sprite(GetPath("VixrStock"));
                    loseAccuracy = 0.099f;
                    maxAccuracyLost = 0.17f;
                    Stockngrip = false;
                    weight = 3.9f;
                }
                else
                {
                    graphic = new Sprite(GetPath("VixrNoStock"));
                    loseAccuracy = 0.13f;
                    maxAccuracyLost = 0.4f;
                    Stockngrip = true;
                    weight = 2f;
                }
            }
            base.Update();
        }
    }
}