#if DEBUG
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class DR300 : BaseGun, IAmAr, IHaveSkin
    {
        private int _postrounds = Rando.ChooseInt(2, 3);
        private int postframe = 8;
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> rounds;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Rounds => rounds;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 8 });
        public DR300(float xval, float yval)
          : base(xval, yval)
        {
            rounds = new EditorProperty<int>(0, this, 0, 2, 1);
            skin = new EditorProperty<int>(8, this, -1f, 9f, 0.5f);
            ammo = _postrounds;
            _ammoType = new AT9mm
            {
                range = 475f,
                accuracy = 0.96f,
                penetration = 0.8f,
                bulletSpeed = 44f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("deleteco/Future/DR300.png"), 37, 11);
            _graphic = _sprite;
            _sprite.frame = postframe;
            _center = new Vec2(18f, 6f);
            _collisionOffset = new Vec2(-18f, -6f);
            _collisionSize = new Vec2(37f, 11f);
            _barrelOffsetTL = new Vec2(12f, 1.5f);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.3f;
            _kickForce = 1f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.9f;
            _holdOffset = new Vec2(-1f, 2f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "Bersa 45";
            _laserOffsetTL = new Vec2(9f, 4f);
            laserSight = true;
			_weight = 1f;
        }
        public override void Update()
        {
            base.Update();
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        public override void EditorPropertyChanged(object property)
        {
            if (rounds.value == 0) _postrounds = Rando.ChooseInt(2, 3);
            if (rounds.value == 1) _postrounds = 2;
            if (rounds.value == 2) _postrounds = 3;
            ammo = _postrounds;
            if ((rounds.value == 0) & (_sprite.frame > 9)) _sprite.frame -= 10;
            if ((rounds.value == 1) & (_sprite.frame < 9)) _sprite.frame += 10;
            if ((rounds.value == 2) & (_sprite.frame < 19)) _sprite.frame += 10;
            _sprite.frame = postframe;
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
#endif