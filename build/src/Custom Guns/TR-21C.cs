using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Shotgun|Custom")]
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class TR21C : Gun
    {
  
        private readonly SpriteMap _sprite;
        public int Teksturka;
        public StateBinding TeksturkaBinding = new StateBinding(nameof(Teksturka));

        public TR21C (float xval, float yval)
            : base(xval, yval)
        {
            ammo = 24;
            _ammoType = new ATMagnum
            {
                range = 110f,
                accuracy = 0.8f,
                penetration = 1f,
                bulletThickness = 0.2f
            };
            _numBulletsPerFire = 9;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("TR-21lmg2p"), 22, 14);
            _graphic = _sprite;
            Teksturka = Rando.Int(0, 3);
            _sprite.frame = Teksturka;
            _center = new Vec2(11f, 6f);
            _collisionOffset = new Vec2(-11f, -6f);
            _collisionSize = new Vec2(22f, 14f);
            _barrelOffsetTL = new Vec2(22f, 4f);
            _fireSound = "shotgunFire";
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(-5f, 2f);
            _editorName = "Crocodile with Extended Mag";
			_weight = 7f;
        }
        public override void Draw()
        {
            _sprite.frame = Teksturka;
            base.Draw();
        }
    }
}