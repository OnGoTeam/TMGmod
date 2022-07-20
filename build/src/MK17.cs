using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class MK17 : BaseAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        private readonly SpriteMap _sprite;

        private float _calculateSide;

        public MK17(float xval, float yval)
            : base(xval, yval)
        {
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
            IntrinsicAccuracy = true;
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
            _kickForce = 1.6f;
            KforceDelta = .3f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.6f;
            _editorName = "Mk17 with Shield";
            _weight = 4.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


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
    }
}
