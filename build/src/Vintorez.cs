using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    public class Vintorez : BaseAr, ISpeedAccuracy
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
		
        public Vintorez(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 10;
            _ammoType = new AT9mmS
            {
                range = 550f,
                accuracy = 0.9f,
                penetration = 1.5f,
                bulletSpeed = 25f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Vintorezpattern"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16.5f, 5.5f);
            _collisionOffset = new Vec2(-16.5f, -5.5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 0.85f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.15f;
            _editorName = "Vintorez";
			_weight = 4.7f;
            MinAccuracy = 0f;
            BaseAccuracy = 0.9f;
            Kforce1Ar = 0.4f;
            Kforce2Ar = 0.85f;
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.5f;
        }
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        
        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; }
    }
}