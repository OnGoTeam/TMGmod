using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Rifle|Burst")]
    // ReSharper disable once InconsistentNaming
    public class AN94C : BaseBurst, IAmAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        private readonly SpriteMap _sprite;

        [UsedImplicitly] public StateBinding StockBinding = new StateBinding(nameof(Silencer));

        public AN94C(float xval, float yval)
            : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Anyx AR2 Mustang"), 33, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(28, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(3f, 2f);
            ShellOffset = new Vec2(0f, -3f);
            ammo = 30;
            _ammoType = new ATCZ { range = 260f, bulletSpeed = 60f, accuracy = 0.87f };
            IntrinsicAccuracy = true;
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 1.2f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f;
            _editorName = "Anyx AR2 Mustang";
            _weight = 5.5f;
            DeltaWait = 0.07f;
            BurstNum = 2;
            Compose(new HSpeedKforce(this, hspeed => hspeed > .1f, kforce => kforce + .83f));
        }

        [UsedImplicitly]
        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/Silenced2.wav");
            set
            {
                if (value)
                {
                    _fireSound = GetPath("sounds/Silenced2.wav");
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    _ammoType = new ATCZS();
                    _barrelOffsetTL = new Vec2(33f, 2f);
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                }
                else
                {
                    _fireSound = "deepMachineGun2";
                    _ammoType = new ATCZ();
                    _barrelOffsetTL = new Vec2(28f, 2f);
                    _sprite.frame %= 10;
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 7 });

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
