using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod.Custom_Guns
{
    /// <inheritdoc cref="BaseBurst"/>
    /// <inheritdoc cref="IHspeedKforce"/>
    /// <inheritdoc cref="IAmAr"/>
    /// <inheritdoc cref="IHaveSkin"/>
    /// <summary>
    /// Has switchable laser
    /// </summary>
    [EditorGroup("TMG|Rifle|Burst|Custom")]
    // ReSharper disable once InconsistentNaming
    public class AN94C : BaseBurst, IHspeedKforce, IAmAr, IHaveSkin
    {
        /// <summary>
        /// whether laser is on
        /// </summary>
        [UsedImplicitly] public bool Laserino;
        /// <summary>
        /// Laser syncing
        /// </summary>
        [UsedImplicitly] public StateBinding LaserBinding = new StateBinding(nameof(Laserino));
        // ReSharper disable once MemberCanBePrivate.Global
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 6, 7 });

        /// <inheritdoc />
        public AN94C(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("AN94C"), 33, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(33f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(3f, 2f);
            ammo = 30;
            _ammoType = new ATMagnum { range = 260f, bulletSpeed = 60f, accuracy = 0.87f };
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 2f;
            Kforce1Ar = 0.07f;
            _kickForce = 0.9f;
            Kforce2Ar = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.1f;
            _editorName = "AN94 with Wooden Stock";
            _weight = 5.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(30f, 2.5f);
            DeltaWait = 0.07f;
            BurstNum = 2;
        }

        /// <inheritdoc />
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Laserino)
                {
                    laserSight = false;
                    _weight = 5.5f;
                    _sprite.frame -= 10;
                    Laserino = false;
                }
                else
                {
                    laserSight = true;
                    _weight = 6f;
                    _sprite.frame += 10;
                    Laserino = true;
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

        /// <inheritdoc />
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        /// <summary>
        /// Updates skin when Skin's changed
        /// </summary>
        /// <param name="property"></param>
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }

        /// <inheritdoc />
        public float Kforce1Ar { get; }

        /// <inheritdoc />
        public float Kforce2Ar { get; }
    }
}