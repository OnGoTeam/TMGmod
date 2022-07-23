using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Burst")]
    // ReSharper disable once InconsistentNaming
    public class SRM1208 : BaseGun, IAmSg, IHaveAllowedSkins
    {
        [UsedImplicitly] public bool Loaded = true;

        [UsedImplicitly] public StateBinding LddBinding = new StateBinding(nameof(Loaded));

        [UsedImplicitly] public bool Shootwasyes;

        [UsedImplicitly] public StateBinding SwyBinding = new StateBinding(nameof(Shootwasyes));

        [UsedImplicitly] public int Yee = 20;

        [UsedImplicitly] public StateBinding YeeBinding = new StateBinding(nameof(Yee));

        [UsedImplicitly] public bool Yeeenabled;

        [UsedImplicitly] public StateBinding YeeeBinding = new StateBinding(nameof(Yeeenabled));

        public SRM1208(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 8;
            SetAmmoType<ATSRM>(.6f);
            _numBulletsPerFire = 8;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("SRM1208"), 29, 10);
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(29f, 3f);
            _flare = new SpriteMap(GetPath("FlareSRM1208"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(0f, 1f);
            _editorName = "SRM 1208";
            _weight = 4.5f;
            ComposeSimpleBurst(2, 1f);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 5, 8 });

        public override void Update()
        {
            _ammoType.barrelAngleDegrees = ammo % 2 == 0 ? -6.5f : +6.5f;
            if (ammo <= 0)
            {
                Loaded = false;
                NonSkin = 0;
            }

            switch (Yeeenabled)
            {
                case true:
                    Yee -= 1;
                    break;
                case false when Yee < 20:
                    Yee = 20;
                    break;
            }

            if (Yee <= 0)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                NonSkin = 0;
                Yeeenabled = false;
                Loaded = true;
            }

            base.Update();
        }

        public override void OnPressAction()
        {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (!Loaded && ammo > 0 && !Yeeenabled)
            {
                if (NonSkin == 0) SFX.Play(GetPath("sounds/tuduc.wav"));
                Yeeenabled = true;
                NonSkin = 1;
            }
            else if (Loaded)
            {
                Fire();
            } else if (ammo == 0)
            {
                DoAmmoClick();
            }
        }

        public override void OnReleaseAction()
        {
            if (Shootwasyes)
            {
                Shootwasyes = false;
                Loaded = false;
            }

            base.OnReleaseAction();
        }

        public override void Fire()
        {
            Shootwasyes = true;
            base.Fire();
        }
    }
}
