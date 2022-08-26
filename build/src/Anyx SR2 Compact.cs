using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class VSK94 : BaseAr, IHaveAllowedSkins
    {
        private float _floatingKickforce;
        [UsedImplicitly] public StateBinding HandAngleOffBinding = new(nameof(HandAngleOff));
        [UsedImplicitly] public float HandAngleOffState;
        [UsedImplicitly] public StateBinding HandAngleOffStateBinding = new(nameof(HandAngleOffState));
        [UsedImplicitly] public float Psevdotimer;
        [UsedImplicitly] public StateBinding PsevdotimerBinding = new(nameof(Psevdotimer));

        public VSK94(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Anyx SR2 Compact";
            ammo = 11;
            SetAmmoType<ATSR2C>();
            MinAccuracy = 0.5f;
            _kickForce = 5.4f;
            KforceDelta = 1.45f;
            
            Smap = new SpriteMap(GetPath("Anyx SR2 Compact"), 32, 10);
            _center = new Vec2(11f, 5f);
            _collisionOffset = -_center;
            _collisionSize = new Vec2(32f, 10f);
            _barrelOffsetTL = new Vec2(32f, 3.5f);
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-2f, -1f);
            _fireSound = GetPath("sounds/new/HighCaliber-Sniper.wav");
            _flare = FrameUtils.FlareOnePixel3();
            _fullAuto = true;
            _fireWait = 1f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.7f;
            _weight = 5.5f;
        }

        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 7, 8 });

        public override void Update()
        {
            _floatingKickforce = Psevdotimer < 16f ? 0.5f : 3f;
            _kickForce = HandleQ() ? _floatingKickforce : 5.4f;
            KforceDelta = HandleQ() ? 0 : 1.45f;
            _kickForce = HandleQ() ? _floatingKickforce : 5.2f;
            loseAccuracy = HandleQ() ? 0f : 0.2f;
            maxAccuracyLost = HandleQ() ? 0f : 0.6f;
            HandAngleOff = HandAngleOffState;
            base.Update();
        }

        public override void OnHoldAction()
        {
            if (ammo > 0)
                HandAngleOff -= 0.0067f;
            else
                HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
            Psevdotimer += 1f;
            base.OnHoldAction();
        }

        public override void OnReleaseAction()
        {
            Psevdotimer = 0f;
            HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
            base.OnReleaseAction();
        }
    }
}
