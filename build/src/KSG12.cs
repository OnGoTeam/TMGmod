using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class Ksg12 : BasePumpAction, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public Ksg12(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 15;
            _ammoType = new ATKSG12();
            _numBulletsPerFire = 8;
            
            _sprite = new SpriteMap(GetPath("KSG12"), 36, 11);
            _graphic = _sprite;
            _center = new Vec2(18f, 6f);
            _collisionOffset = new Vec2(-18f, -6f);
            _collisionSize = new Vec2(36f, 11f);
            _barrelOffsetTL = new Vec2(36f, 3f);
            _holdOffset = new Vec2(-1f, 1f);
            _fireSound = "shotgunFire2";
            _kickForce = 3.75f;
            _manualLoad = true;
            _fireWait = 2.5f;
            LoaderSprite = new SpriteMap(GetPath("KSG12Pimp"), 14, 6)
            {
                center = new Vec2(7f, 3f),
            };
            FrameId = 0;
            ShellOffset = new Vec2(-8f, 0f);
            _editorName = "KSG-12";
            LoaderVec2 = new Vec2(6f, 0f);
            Loaddx = 2.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set
            {
                SetSpriteMapFrameId(_sprite, value, 10 * NonSkinFrames);
                SetSpriteMapFrameId(LoaderSprite, value, 10);
            }
        }
    }
}
