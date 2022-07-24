using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class SKS : BaseGun, IHaveAllowedSkins, I5
    {
        private int _bullets;
        private int _patrons = 12;

        public bool Stick;

        [UsedImplicitly] public StateBinding StickBinding = new StateBinding(nameof(Stick));

        public SKS(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "SKS";
            ammo = 11;
            _ammoType = new AT762NATO
            {
                range = 800f,
                accuracy = 0.97f,
                bulletSpeed = 95f,
                bulletThickness = 1.5f,
            };
            MaxAccuracy = 0.97f;
            MinAccuracy = 0.6f;
            Smap = new SpriteMap(GetPath("SKS"), 46, 11);
            _center = new Vec2(23f, 6f);
            _collisionOffset = new Vec2(-23f, -6f);
            _collisionSize = new Vec2(46f, 11f);
            _barrelOffsetTL = new Vec2(42f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(8f, 0f);
            ShellOffset = new Vec2(-9f, -2f);
            _fireSound = GetPath("sounds/scar.wav");
            _flare.center = new Vec2(0f, 5f);
            _fullAuto = false;
            _fireWait = 1.55f;
            _kickForce = 3.8f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            _weight = 6f;
            Compose(new SpeedAccuracy(this, 1f, 1f, .15f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });

        private void OnQuackPress()
        {
            if (ammo < 12)
            {
                _patrons = ammo;
                _bullets = _patrons + 20;
                ammo += 20;
            }

            _fireSound = "";
            _flare = new SpriteMap(GetPath("takezis"), 4, 4)
            {
                center = new Vec2(0f, 0f),
            };
            _ammoType = new ATNB();
            _fireWait = 10f;
            _barrelOffsetTL = new Vec2(0f, 6f);
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _kickForce = 0f;
            Stick = true;
            Fire();
        }

        private void OnQuackHold()
        {
            _holdOffset = new Vec2(12f, 0f);
            if (ammo < _bullets) ammo += 1;
        }

        private void OnQuackRelease()
        {
            ResetAmmoType();
        }

        private void UpdateWithDuck()
        {
            if (duck.inputProfile.Pressed("QUACK")) OnQuackPress();
            if (duck.inputProfile.Down("QUACK")) OnQuackHold();
            if (duck.inputProfile.Released("QUACK")) OnQuackRelease();
        }

        private void CustomUpdate()
        {
            if (ammo < 12) _patrons = ammo;
            if (duck != null) UpdateWithDuck();
        }

        public override void Update()
        {
            base.Update();
            CustomUpdate();
        }

        private void ResetAmmoType()
        {
            ammo = _patrons;
            _ammoType = new AT762NATO
            {
                range = 800f,
                accuracy = 0.97f,
                bulletSpeed = 95f,
                bulletThickness = 1.5f,
            };
            _fireWait = 1.55f;
            _barrelOffsetTL = new Vec2(42f, 4f);
            _fireSound = GetPath("sounds/scar.wav");
            _holdOffset = new Vec2(8f, 0f);
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            _kickForce = 4.8f;
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            Stick = false;
        }

        private void CustomThrown()
        {
            if (ammo != 0) ResetAmmoType();

            if (Stick && _patrons == 0) ammo = 0;
        }

        public override void Thrown()
        {
            CustomThrown();
            base.Thrown();
        }
    }
}
