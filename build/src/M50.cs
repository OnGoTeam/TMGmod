using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    public class M50 : BaseGun, ISpeedAccuracy, IAmSr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });

        public M50(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 7;
            _ammoType = new Cal50Explode
            {
                range = 1100f,
                accuracy = 1f,
                penetration = 1f,
                bulletThickness = 2.5f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("M50"), 40, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20f, 6.5f);
            _collisionOffset = new Vec2(-20f, -6.5f);
            _collisionSize = new Vec2(40f, 13f);
            _barrelOffsetTL = new Vec2(40f, 6f);
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 3.75f;
            _kickForce = 8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(6f, -1f);
            laserSight = true;
            _laserOffsetTL = new Vec2(31f, 9f);
            _editorName = "M50";
            _weight = 6.75f;
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