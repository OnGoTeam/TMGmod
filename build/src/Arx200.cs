using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    public class Arx200 : BaseDmr, IHaveSkin
    {
        private const int NonSkinFrames = 1;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 5, 6, 7, 9 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public Arx200(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new ATArx200();
            MaxAccuracy = 0.98f;
            MinAccuracy = 0.6f;
            RegenAccuracyDmr = 0.009f;
            DrainAccuracyDmr = 0.1f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("ARX200"), 33, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-17f, -7f);
            _collisionSize = new Vec2(33f, 14f);
            _barrelOffsetTL = new Vec2(33f, 6f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(1f, -1f);
            ShellOffset = new Vec2(-4f, -2f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.65f;
            _kickForce = 3.1f;
            loseAccuracy = 0f; //0.15f
            maxAccuracyLost = 0f; //0.15f
            _editorName = "Beretta ARX-200";
            _weight = 6f;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic)) bublic = Rando.Int(0, 9);
            _sprite.frame = bublic;
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
