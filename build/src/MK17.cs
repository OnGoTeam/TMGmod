using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class MK17 : BaseAr, IHaveSkin
    {
        private const int NonSkinFrames = 2;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 5 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private float _calculateSide;

        public MK17(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _hitPoints = 49f;
            thickness = 12f;
            ammo = 20;
            _ammoType = new ATMK17
            {
                range = 345f,
                accuracy = 0.84f,
                bulletSpeed = 35f,
                bulletThickness = 0.6f,
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Mk17Shield"), 26, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(5f, 8f);
            _collisionOffset = -_center;
            _collisionSize = new Vec2(26f, 12f);
            _barrelOffsetTL = new Vec2(26f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-3f, 1f);
            ShellOffset = new Vec2(0f, -3f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.81f;
            _kickForce = 2.35f;
            KickForceSlowAr = 1.6f;
            KickForceFastAr = 1.9f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.6f;
            _editorName = "Mk17 with Shield";
            _weight = 4.5f;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            _calculateSide = DamageImplementation.Calculate(bullet);
            MakeDamage();
            return Hit(bullet, hitPos);
        }

        private void MakeDamage()
        {
            _hitPoints -= _calculateSide;
            if (!(_hitPoints <= 0f)) return;
            _sprite.frame %= 10;
            _sprite.frame += 10;
            thickness = 0f;
        }

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic)) bublic = Rando.Int(0, 9);
            _sprite.frame = bublic;
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
