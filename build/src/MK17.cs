using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.DamageLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class MK17 : BaseAr, IHaveAllowedSkins
    {
        private float _calculateSide;

        public MK17(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Mk17 with Shield";
            _hitPoints = 49f;
            thickness = 12f;
            ammo = 20;
            SetAmmoType<ATMK17>();
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("Mk17Shield"), 26, 12);
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
            _weight = 4.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5 });

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
            NonSkin = 1;
            thickness = 0f;
        }
    }
}
