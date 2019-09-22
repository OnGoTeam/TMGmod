#if DEBUG
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    public class DR300 : BaseGun, IAmAr, IHaveSkin
    {
        private readonly int _postrounds;
        private int postframe = 28;
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
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
            _postrounds = Rando.ChooseInt(20, 30);
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            rounds = new EditorProperty<int>(1, this, 0, 2, 1);
            if (rounds == 1) _postrounds = 20;
            if (rounds == 1) postframe = 8;
            if (rounds == 2) _postrounds = 30;
            if (rounds == 2) postframe = 18;
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
            _center = new Vec2(6f, 4f);
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(12f, 8f);
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
            if (_sprite.frame == 28) 
            base.Update();
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                if (rounds.value == 30) bublic = Rando.Int(0, 9);
                else if (rounds.value == 20) bublic = Rando.Int(10, 19);
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
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
#endif