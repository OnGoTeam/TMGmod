using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    [PublicAPI]
    public class GarlandGun : Gun
    {
        
        private float _sprwait;
        public int FrameId;
        public StateBinding FrameBinding = new StateBinding(nameof(FrameId));

        public GarlandGun(float xval, float yval) : base(xval, yval)
        {
            ammo = 24;
            _type = "gun";
            _graphic = new Sprite(GetPath("Holiday/Garlandun"));
            _center = new Vec2(11f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(22f, 11f);
            _barrelOffsetTL = new Vec2(22f, 6f);
            _holdOffset = new Vec2(2f, -1f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.65f;
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _editorName = "Garlandun";
            _weight = 1f;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _ammoType = new AT9mmParasha();
        }
        public override void Update()
        {
            _sprwait -= 0.11f;
            if (_sprwait <= 0f)
            {
                var randres = Rando.Int(0, 29);
                while (randres == ((AT9mmParasha) ammoType).SpriteY.frame)
                {
                    randres = Rando.Int(0, 29);
                }
                FrameId = randres;
                _sprwait += 1.0f;
            }

            ((AT9mmParasha) ammoType).SpriteY.frame = FrameId;
            base.Update();
        }
    }
}