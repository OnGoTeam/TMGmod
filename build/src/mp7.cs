using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    //[yee] switch
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class MP7 : BaseGun, IAmSmg, IHaveSkin
    {
        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }
        [UsedImplicitly]
        public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public bool ducklookleft = false;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7 });

        public MP7(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 35;
            _ammoType = new AT9mmS
            {
                range = 175f,
                accuracy = 0.9f
            };
            BaseAccuracy = 0.9f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MP7"), 20, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(12f, 4f);
            _collisionOffset = new Vec2(-12f, -4f);
            _collisionSize = new Vec2(20f, 10f);
            _barrelOffsetTL = new Vec2(20f, 2f);
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 1.5f;
            _holdOffset = new Vec2(3f, 1f);
            ShellOffset = new Vec2(-6f, -2f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _editorName = "HK MP7";
            _weight = 3f;
        }

        public override void Update()
        {
            base.Update();

            if (offDir > 0)
            {
                if (ducklookleft == true)
                {
                    HandAngleOff = -HandAngleOff;
                    ducklookleft = false;
                }
                if (duck?.inputProfile.Down("UP") == true && !_raised)
                {
                    if (HandAngleOff > -0.7f) HandAngleOff -= 0.05f;

                    return;
                }

                if (duck?.inputProfile.Down("QUACK") == true && !_raised && !duck.sliding)
                {
                    if (HandAngleOff < 0.7f) HandAngleOff += 0.05f;

                    return;
                }
            }
            else if (offDir < 0)
            {
                if (ducklookleft == false)
                {
                    HandAngleOff = -HandAngleOff;
                    ducklookleft = true;
                }
                if (duck?.inputProfile.Down("UP") == true && !_raised)
                {
                    if (HandAngleOff > -0.6f) HandAngleOff -= 0.05f;

                    return;
                }

                if (duck?.inputProfile.Down("QUACK") == true && !_raised && !duck.sliding)
                {
                    if (HandAngleOff < 0.6f) HandAngleOff += 0.05f;

                    return;
                }
            }

            if (handAngle > 0f) handAngle -= 0.1f;
            else if (handAngle < 0f) handAngle += 0.1f;
            if ((handAngle > -0.1f) & (handAngle < 0.1f) & (handAngle != 0f)) handAngle = 0f;
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
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
