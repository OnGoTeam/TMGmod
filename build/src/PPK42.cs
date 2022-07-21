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
    public class PPK42 : BaseSmg, IHaveAllowedSkins, I5
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public PPK42(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATPPK42();
            MaxAccuracy = 0.8f;
            KforceDelta = 2.5f;
            KforceDelay = 20;
            
            _sprite = new SpriteMap(GetPath("PPK42"), 25, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(25f, 11f);
            _barrelOffsetTL = new Vec2(25f, 2f);
            _flare = new SpriteMap(GetPath("FlarePPK42"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 1.5f;
            _holdOffset = new Vec2(4f, 2f);
            ShellOffset = new Vec2(-3f, -4f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _editorName = "PPK 42";
            _weight = 3f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
