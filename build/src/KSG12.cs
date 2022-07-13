using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class Ksg12 : BasePumpAction, IHaveSkin
    {
        private const int NonSkinFrames = 1;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public Ksg12(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 15;
            _ammoType = new ATKSG12();
            _numBulletsPerFire = 8;
            _type = "gun";
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
                center = new Vec2(7f, 3f)
            };
            FrameId = 0;
            ShellOffset = new Vec2(-8f, 0f);
            _editorName = "KSG-12";
            LoaderVec2 = new Vec2(6f, 0f);
            Loaddx = 2.5f;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

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

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic)) bublic = Rando.Int(0, 9);
            FrameId = bublic;
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
