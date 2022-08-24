using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Burst")]
    [UsedImplicitly]
    public class M93R : BaseGun, IAmHg, IHaveAllowedSkins, I5
    {
        public M93R(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "M93R";
            Smap = new SpriteMap(GetPath("M93R"), 12, 9);
            _center = new Vec2(6f, 2f);
            _collisionOffset = new Vec2(-6f, -2f);
            _collisionSize = new Vec2(12f, 9f);
            ammo = 15;
            SetAmmoType<ATM93R>();
            _barrelOffsetTL = new Vec2(12f, 1.5f);
            _flare = FrameUtils.FlareOnePixel0();
            _holdOffset = new Vec2(-1f, 0f);
            _fireSound = GetPath("sounds/new/SMG-1.wav");
            _fullAuto = false;
            _fireWait = 1.5f;
            _kickForce = 0.24f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.35f;
            ShellOffset = new Vec2(-3f, 0f);
            _weight = 2f;
            ComposeSimpleBurst(3, .3f);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5 });
    }
}
