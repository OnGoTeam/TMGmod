using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class KS23 : BasePumpAction
    {
        public bool shootwasyes;
        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }
        [UsedImplicitly]
        public float HandAngleOffState;
        [UsedImplicitly]
        public StateBinding HandAngleOffStateBinding = new StateBinding(nameof(HandAngleOffState));
        [UsedImplicitly]
        public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));
        [UsedImplicitly]
        public StateBinding ShootwasyesBinding = new StateBinding(nameof(shootwasyes));
        [UsedImplicitly]
        public KS23(float xval, float yval) : base(xval, yval)
	    {
		    ammo = 6;
	        _ammoType = new AT9mmS
	        {
	            range = 169f,
	            accuracy = 0.33f,
	            penetration = 1f,
	            bulletSpeed = 50f,
	            bulletThickness = 0.6f
	        };
            _numBulletsPerFire = 16;
            _type = "gun";
		    _graphic = new SpriteMap(GetPath("KS-23"), 35, 8);
		    _center = new Vec2(18f, 4f);
		    _collisionOffset = new Vec2(-18f, -4f);
		    _collisionSize = new Vec2(35f, 8f);
		    _barrelOffsetTL = new Vec2(35f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(8f, 1f);
            _fireSound = GetPath("sounds/shotgun.wav");
            _kickForce = 9f;
            loseAccuracy = 0.4f;
            maxAccuracyLost = 0.9f;
		    _manualLoad = true;
            _fireWait = 4f;
            _editorName = "KS-23";
            LoaderSprite = new SpriteMap(GetPath("KS-23Pump"), 7, 4)
            {
                    center = new Vec2(4f, 2f)
            };
            LoaderVec2 = new Vec2(2f, 0f);
            Loaddx = 3f;
            LoadSpeed = 4;
        }
        public override void Fire()
        {
            shootwasyes = true;
            if (ammo < 1) shootwasyes = false;
            base.Fire();
        }
        public override void Update()
        {
            HandAngleOff = HandAngleOffState;
            base.Update();
            if (shootwasyes) HandAngleOff -= 0.075f;
            if (((HandAngleOff > -1f) & (HandAngleOff < -0.8f)) || ((!shootwasyes) & (HandAngleOff < 0f)))
            {
                HandAngleOff += Rando.Float(0.02f, 0.0367f);
                shootwasyes = false;
            }
            if (HandAngleOff > 0f) HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
        }
    }
}