using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    public class SpectreM4 : BaseSmg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 6 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public SpectreM4(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATSpectreM4();
            IntrinsicAccuracy = true;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SpectreM4"), 19, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(9.5f, 5f);
            _collisionOffset = new Vec2(-9.5f, -5f);
            _collisionSize = new Vec2(19f, 10f);
            _barrelOffsetTL = new Vec2(13f, 1f);
            _fireSound = GetPath("sounds/smg.wav");
            _flare = new SpriteMap("smallFlare", 11, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fullAuto = true;
            _fireWait = 0.31f;
            _kickForce = 0.8f;
            KforceDelta = 2.7f;
            KforceDelay = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.34f;
            _holdOffset = new Vec2(3f, 3f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "Spectre M4";
            _weight = 3.3f;
        }

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/SilencedPistol.wav");
            set
            {
                if (value)
                {
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                    _ammoType = new ATSpectreM4S();
                    _barrelOffsetTL = new Vec2(16f, 1f);
                    loseAccuracy = 0.07f;
                    maxAccuracyLost = 0.3f;
                    weight = 3.8f;
                    _fireSound = GetPath("sounds/SilencedPistol.wav");
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                }
                else
                {
                    _sprite.frame %= 10;
                    _flare = new SpriteMap("smallFlare", 11, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                    _fireSound = GetPath("sounds/smg.wav");
                    _ammoType = new ATSpectreM4();
                    _barrelOffsetTL = new Vec2(13f, 1f);
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.34f;
                    _weight = 3.3f;
                }
            }
        }

        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                SFX.Play(Silencer ? GetPath("sounds/silencer_off.wav") : GetPath("sounds/silencer_on.wav"));
                Silencer = !Silencer;
                SFX.Play("quack", -1);
            }

            base.Update();
        }
    }
}
