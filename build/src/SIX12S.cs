using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    // ReSharper disable once InconsistentNaming
    public class SIX12S : Gun, IHaveSkin
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
		
		public SIX12S (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new AT9mmS
            {
                range = 225f,
                accuracy = 0.9f,
                penetration = 1f
            };
            _numBulletsPerFire = 14;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SIX12Spattern"), 29, 10);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(19.5f, 5f);
            collisionOffset = new Vec2(-19.5f, -5f);
            collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 1.7f;
            _kickForce = 5f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(24f, 7f);
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SIX12 Silenced";
			weight = 4f;
        }
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}