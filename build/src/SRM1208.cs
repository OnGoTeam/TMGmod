using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Burst")]
    // ReSharper disable once InconsistentNaming
    public class SRM1208 : BaseBurst, IAmSg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        private readonly SpriteMap _sprite;

        [UsedImplicitly] public StateBinding LddBinding = new StateBinding(nameof(Loaded));

        [UsedImplicitly] public bool Loaded = true;

        [UsedImplicitly] public bool Shootwasyes;

        [UsedImplicitly] public StateBinding SwyBinding = new StateBinding(nameof(Shootwasyes));

        [UsedImplicitly] public int Yee = 20;

        [UsedImplicitly] public StateBinding YeeBinding = new StateBinding(nameof(Yee));

        [UsedImplicitly] public StateBinding YeeeBinding = new StateBinding(nameof(Yeeenabled));

        [UsedImplicitly] public bool Yeeenabled;

        public SRM1208(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 8;
            _ammoType = new ATSRM1();
            MaxAccuracy = 0.6f;
            _numBulletsPerFire = 8;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SRM1208"), 29, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
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
            DeltaWait = 1f;
            BurstNum = 2;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 5, 8 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            if (ammo % 2 == 0) _ammoType = new ATSRM1();
            else _ammoType = new ATSRM2();
            if (ammo <= 0)
            {
                Loaded = false;
                _sprite.frame %= 10;
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
                _sprite.frame %= 10;
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
                if (_sprite.frame < 10) SFX.Play(GetPath("sounds/tuduc.wav"));
                Yeeenabled = true;
                _sprite.frame = _sprite.frame % 10 + 10;
            }
            else if (Loaded)
            {
                Fire();
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
