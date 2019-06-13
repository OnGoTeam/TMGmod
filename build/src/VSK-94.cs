using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    public class VSK94 : BaseAr, ISpeedAccuracy, IHaveSkin
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 7 });

        public VSK94(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new AT9mmS
            {
                range = 550f,
                accuracy = 0.9f,
                bulletSpeed = 25f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("VSK-94"), 32, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-16f, -5f);
            _collisionSize = new Vec2(32f, 10f);
            _barrelOffsetTL = new Vec2(32f, 5f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 2.85f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.15f;
            _editorName = "VSK-94";
            _weight = 4f;
            MinAccuracy = 0f;
            BaseAccuracy = 0.9f;
            Kforce1Ar = 0.4f;
            Kforce2Ar = 0.85f;
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.5f;
        }
        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}