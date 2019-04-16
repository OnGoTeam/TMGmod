using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class UziPro : BaseSmg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public bool Silencer;
        public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 4, 6, 8 });
        public UziPro (float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new AT9mm
            {
                range = 70f,
                accuracy = 0.9f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("UziProSpattern"), 16, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(11f, 3f);
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.4f;
            _kickForce = 0.5f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.5f;
            _holdOffset = new Vec2(2f, 0f);
            laserSight = true;
            _laserOffsetTL = new Vec2(9f, 6f);
            _editorName = "Uzi Pro";
			_weight = 2.5f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Silencer)
                {
                    _sprite.frame -= 10;
                    _ammoType = new AT9mm
                    {
                        range = 70f,
                        accuracy = 0.9f,
                        penetration = 1f
                    };
                    _barrelOffsetTL = new Vec2(11f, 3f);	
                    Silencer = false;
                    _flare = new SpriteMap("smallFlare", 11, 10)
                    {
                        center = new Vec2(0.0f, 5f)
                    };
                    _fireSound = GetPath("sounds/smg.wav");
                }
                else
                {
                    _sprite.frame += 10;
                    _ammoType = new AT9mmS
                    {
                        range = 100f,
                        accuracy = 1f
                    };
                    _barrelOffsetTL = new Vec2(17f, 3f);			 
                    Silencer = true;
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    _fireSound = GetPath("sounds/SilencedPistol.wav");
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}