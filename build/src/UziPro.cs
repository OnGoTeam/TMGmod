using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|MP")]
    // ReSharper disable once InconsistentNaming
    public class UziPro : BaseSmg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        private readonly SpriteMap _sprite;

        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public UziPro(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 24;
            _ammoType = new ATUzi();
            IntrinsicAccuracy = true;
            KforceDelay = 25;
            KforceDelta = 4f;

            _sprite = new SpriteMap(GetPath("UziProS"), 16, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(10f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.4f;
            _kickForce = 0.5f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.5f;
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-5f, -3f);
            laserSight = true;
            _laserOffsetTL = new Vec2(9f, 6f);
            _editorName = "Uzi Pro";
            _weight = 2.5f;
        }

        protected override string HintMessage => "silencer";

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/SilencedPistol.wav");
            set
            {
                if (value)
                {
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                    _ammoType = new ATUziS();
                    _barrelOffsetTL = new Vec2(16f, 2f);
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    _fireSound = GetPath("sounds/SilencedPistol.wav");
                }
                else
                {
                    _sprite.frame %= 10;
                    _ammoType = new ATUzi();
                    _barrelOffsetTL = new Vec2(10f, 2f);
                    _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                    _fireSound = GetPath("sounds/smg.wav");
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 4, 6, 8 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
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
