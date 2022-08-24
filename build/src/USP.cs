using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class USP : BaseGun, IAmHg, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public USP(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "USP-S";
            ammo = 13;
            SetAmmoType<ATUSP>();
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("USP"), 23, 9);
            _center = new Vec2(8f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(23f, 9f);
            _barrelOffsetTL = new Vec2(14f, 2.5f);
            _fireSound = GetPath("sounds/new/USP.wav");
            _flare = FrameUtils.FlareOnePixel0();
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 1f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            _holdOffset = new Vec2(1f, 0f);
            ShellOffset = new Vec2(-5f, 0f);
            _weight = 1f;
            Compose(new Quacking(this, true, true, () => Silencer = !Silencer));
        }

        public override string HintMessage => "silencer";

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/SilencedPistol.wav");
            set
            {
                if (value != Silencer)
                    FrameUtils.SwitchedSilencer(Silencer);
                NonSkin = value ? 1 : 0;
                _flare = value ? FrameUtils.TakeZis() : FrameUtils.FlareOnePixel0();
                _fireSound = value ? GetPath("sounds/SilencedPistol.wav") : GetPath("sounds/new/USP.wav");
                if (value)
                    SetAmmoType<ATUSPS>();
                else
                    SetAmmoType<ATUSP>();
                _barrelOffsetTL = value ? new Vec2(23f, 2.5f) : new Vec2(14f, 2.5f);
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 4, 7 });
    }
}
