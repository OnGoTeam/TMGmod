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
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        public PPSh41(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 71;
            _ammoType = new ATPPSH41();
            MaxAccuracy = 0.73f;
            KforceDelta = 3f;
            KforceDelay = 50;
            
            _sprite = new SpriteMap(GetPath("PPSH41"), 30, 8);
            _graphic = _sprite;
            _center = new Vec2(15f, 4f);
            _collisionOffset = new Vec2(-15f, -4f);
            _collisionSize = new Vec2(30f, 8f);
            _barrelOffsetTL = new Vec2(29f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun2";
            _fireWait = 0.25f;
            _kickForce = 1.7f;
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(0f, -3f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.4f;
            _editorName = "PPSh 41";
            _weight = 3.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
