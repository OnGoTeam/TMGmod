using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Custom_Guns
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG|Custom")]
    // ReSharper disable once InconsistentNaming
    public class PPShC : Gun
    {
  
        private readonly SpriteMap _sprite;
        public int Teksturka;
        public StateBinding TeksturkaBinding = new StateBinding(nameof(Teksturka));

        public PPShC(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 35;
            _ammoType = new AT9mm
            {
                range = 300f,
                accuracy = 0.9f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PPshlowmagptr"), 48, 11);
            graphic = _sprite;
            Teksturka = Rando.Int(0, 5);
            _sprite.frame = Teksturka;
            center = new Vec2(23f, 5.5f);
            collisionOffset = new Vec2(-23f, -4.5f);
            collisionSize = new Vec2(46f, 11f);
            _barrelOffsetTL = new Vec2(47f, 4f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.25f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(7f, -1f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.2f;
            _editorName = "PPSh with Low Mag";
			weight = 5.5f;
        }
        public override void Draw()
        {
            _sprite.frame = Teksturka;
            base.Draw();
        }
    }
}