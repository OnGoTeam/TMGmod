using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class Remington : BasePumpAction, IHaveAllowedSkins
    {
        public Remington(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Fabarm FP-6";
            ammo = 6;
            SetAmmoType<ATFABARM>();
            _numBulletsPerFire = 5;
            Smap = new SpriteMap(GetPath("Fabarm FP-6"), 33, 9);
            LoaderSprite = new SpriteMap(GetPath("Fabarm FP-6Pump"), 9, 4)
            {
                center = new Vec2(5f, 2f),
            };
            FrameId = 0;
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(33f, 1.5f);
            _flare = new SpriteMap(GetPath("FlareBase2"), 13, 10)
            {
                center = new Vec2(0f, 4.5f),
            };
            _holdOffset = new Vec2(1f, 2f);
            _fireSound = "shotgunFire";
            _kickForce = 3f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.8f;
            _manualLoad = true;
            _fireWait = 3f;
            ShellOffset = new Vec2(2f, -3f);
            LoaderVec2 = new Vec2(9f, -1f);
            Loaddx = 3f;
            LoadSpeed = 15;
            _weight = 3.2f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 4 });

        protected override void UpdateFrameId(int frameId)
        {
            SetSpriteMapFrameId(LoaderSprite, frameId, SkinFrames);
        }
    }
}