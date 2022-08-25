using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    public class Vixr : BaseGun, IAmAr, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));

        [UsedImplicitly] public float HandAngleOffState;

        [UsedImplicitly] public StateBinding HandAngleOffStateBinding = new StateBinding(nameof(HandAngleOffState));

        public Vixr(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Anyx ARS Elite";
            ammo = 21;
            SetAmmoType<ATARS>();
            
            // THIS FILE HAS REBORN TREE TIMES SQUARES!! send this massage to your friends or not to friends
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("Anyx ARS Elite"), 33, 9);
            _center = new Vec2(17f, 5f);
            _collisionOffset = -_center;
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(28f, 2.5f);
            _holdOffset = new Vec2(4f, 2f);
            ShellOffset = new Vec2(-5f, -2f);
            _fireSound = GetPath("sounds/new/HighCaliber-Impactful.wav");
            _flare = new SpriteMap(GetPath("FlareAnyxARS"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 3f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            _weight = 6f;
            handAngle = 0f;
            Compose(
                new WithStock(
                    this,
                    true,
                    GetPath("sounds/beepods1"),
                    GetPath("sounds/beepods2"),
                    1f / 10f,
                    state =>
                    {
                        _kickForce = state.Deployed ? 3f : 4.6f;
                        _fireWait = state.Deployed ? 0.75f : 0.6f;
                        loseAccuracy = state.Deployed ? 0.15f : 0.2f;
                        maxAccuracyLost = state.Deployed ? 0.3f : 0.6f;
                        _weight = state.Deployed ? 6f : 3.5f;
                        NonSkin = state.Deployed ? 0 : state.Folded ? 2 : 1;
                    }
                ).Switching()
            );
        }

        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 6, 8 });

        public override void Update()
        {
            HandAngleOff = HandAngleOffState;
            base.Update();
        }

        public override void OnHoldAction()
        {
            if (ammo > 0) HandAngleOff -= 0.01f;
            else if (ammo < 1) HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
            base.OnHoldAction();
        }

        public override void OnReleaseAction()
        {
            HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
            base.OnReleaseAction();
        }
    }
}
