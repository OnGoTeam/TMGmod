using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class PPSh41 : BaseSmg, I5, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public PPSh41(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "PPSh 41";
            ammo = 71;
            SetAmmoType<ATPPSH41>();
            KforceDelta = 3f;
            KforceDelay = 50;
            Smap = new SpriteMap(GetPath("PPSH41"), 30, 8);
            _center = new Vec2(15f, 4f);
            _collisionOffset = new Vec2(-15f, -4f);
            _collisionSize = new Vec2(30f, 8f);
            _barrelOffsetTL = new Vec2(30f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/new/SMG-2.wav");
            _fireWait = 0.25f;
            _kickForce = 1.7f;
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(2f, -2f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.4f;
            _weight = 3.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
    }
}
