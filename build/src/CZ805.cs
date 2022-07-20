using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class CZ805 : BaseAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 10;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 4, 5, 7 });

        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public CZ805(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATCZ();
            MaxAccuracy = _ammoType.accuracy;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("CZ805Bren"), 41, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20.5f, 5.5f);
            _collisionOffset = new Vec2(-20.5f, -5.5f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(5f, 1f);
            ShellOffset = new Vec2(-5f, -3f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f;
            _editorName = "CZ-805 BREN";
            _weight = 5f;
            KickForceSlowAr = 1.5f;
            KickForceFastAr = 2.76f;
        }

        [UsedImplicitly]
        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/Silenced2.wav");
            set
            {
                if (value)
                {
                    _sprite.frame %= 50;
                    _sprite.frame += 50;
                    _fireSound = GetPath("sounds/Silenced2.wav");
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    _ammoType = new ATCZS();
                    _barrelOffsetTL = new Vec2(42.5f, 3f);
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.35f;
                }
                else
                {
                    _sprite.frame %= 50;
                    _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                    _fireSound = "deepMachineGun2";
                    _ammoType = new ATCZ();
                    _barrelOffsetTL = new Vec2(39f, 3f);
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.25f;
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

            if (ammo > 20 && ammo <= 26 && FrameId / 10 % 5 != 1) _sprite.frame += 10;
            if (ammo > 12 && ammo <= 20 && FrameId / 10 % 5 != 2) _sprite.frame += 10;
            if (ammo > 5 && ammo <= 12 && FrameId / 10 % 5 != 3) _sprite.frame += 10;
            if (ammo > 0 && ammo <= 5 && FrameId / 10 % 5 != 4) _sprite.frame += 10;
            base.Update();
        }
    }
}
