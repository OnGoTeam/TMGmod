using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class PPSh41 : BaseSmg, I5, IShowSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public SpriteMap ShowedSkin(int allowed) => new SpriteMap(GetPath("PPSH41"), 30, 8) { _frame = allowed };

        public PPSh41(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f, "random");
            ammo = 71;
            _ammoType = new ATPPSH41();
            MaxAccuracy = 0.73f;
            KickForceDeltaSmg = 3f;
            MaxDelaySmg = 50;
            _type = "gun";
            _sprite = ShowedSkin(0);
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
            _fullAuto = true;
            _fireWait = 0.25f;
            _kickForce = 1.7f;
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(0f, -3f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.4f;
            _editorName = "PPSh 41";
            _weight = 3.5f;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
