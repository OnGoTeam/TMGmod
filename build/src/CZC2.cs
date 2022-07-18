using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class CZC2 : BaseAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public CZC2(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 23;
            _ammoType = new ATCZ2();
            MaxAccuracy = _ammoType.accuracy;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("CZC2"), 41, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20.5f, 5.5f);
            _collisionOffset = new Vec2(-20.5f, -5.5f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(37f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(5f, 1f);
            ShellOffset = new Vec2(-5f, -3f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 0.9f;
            loseAccuracy = 0.12f;
            maxAccuracyLost = 0.25f;
            _editorName = "CZ-C2 SAR";
            _weight = 4.4f;
            KickForceSlowAr = 1.5f;
            KickForceFastAr = 3.1f;
        }

        [UsedImplicitly]
        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/Silenced2.wav");
            set
            {
                if (value)
                {
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                    _fireSound = GetPath("sounds/Silenced2.wav");
                    _ammoType = new ATCZS2();
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.28f;
                    _barrelOffsetTL = new Vec2(41f, 3f);
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                }
                else
                {
                    _sprite.frame %= 10;
                    _fireSound = "deepMachineGun2";
                    _ammoType = new ATCZ2();
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.3f;
                    _barrelOffsetTL = new Vec2(37f, 3f);
                    _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                }
            }
        }

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
