using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Sniper|Custom")]
    public class VintorezC : BaseGun, ISpeedAccuracy, IHspeedKforce
    {
  
        private readonly SpriteMap _sprite;
        public int Teksturka;
        public StateBinding TeksturkaBinding = new StateBinding(nameof(Teksturka));

        public VintorezC(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new AT9mmS
            {
                range = 550f,
                accuracy = 0.9f,
                penetration = 1.5f,
                bulletSpeed = 25f
            };
            _type = "gun";
			//I AM A GREEN TEXT
            _sprite = new SpriteMap(GetPath("Vintorezexmagptr"), 33, 12);
            graphic = _sprite;
            Teksturka = Rando.Int(0, 3);
            _sprite.frame = Teksturka;
            center = new Vec2(16.5f, 6f);
            collisionOffset = new Vec2(-16.5f, -6f);
            collisionSize = new Vec2(33f, 12f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 0.85f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.15f;
            _editorName = "Vintorez with Extended Mag";
			weight = 4.7f;
            MinAccuracy = 0f;
            BaseAccuracy = 0.9f;
            Kforce1Ar = 0.4f;
            Kforce2Ar = 0.85f;
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.5f;
        }
        public override void Draw()
        {
            _sprite.frame = Teksturka;
            base.Draw();
        }

        public float Kforce1Ar { get; }
        public float Kforce2Ar { get; }
        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; }
    }
}