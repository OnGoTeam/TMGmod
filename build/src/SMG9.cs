using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class SMG9 : BaseSmg, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public SMG9(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "SMG-9";
            ammo = 24;
            SetAmmoType<ATSMG9>();
            KforceDelta = 3.5f;
            KforceDelay = 15;
            Smap = new SpriteMap(GetPath("SMG9"), 16, 15);
            _center = new Vec2(7f, 7f);
            _collisionOffset = new Vec2(-8f, -7f);
            _collisionSize = new Vec2(16f, 15f);
            _barrelOffsetTL = new Vec2(16f, 3.5f);
            _flare = FrameUtils.FlareOnePixel0();
            _fireSound = GetPath("sounds/new/SMG-3.wav");
            _fullAuto = true;
            _fireWait = 0.35f;
            _kickForce = 1.6f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.57f;
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-1f, -3f);
            _weight = 2.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4 });
    }
}
