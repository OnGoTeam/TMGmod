using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using TMGmod.Core.AmmoTypes;


namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Break-Action")]
    [BaggedProperty("canSpawn", false)]
    public class SkeetGun:BaseGun, IHaveSkin, IAmSg
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        private bool _ducklookleft;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 4, 6, 7, 9 });
        public SkeetGun(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 2;
            _ammoType = new ATSkeetGun();
            BaseAccuracy = 0.9f;
            _numBulletsPerFire = 10;
            _sprite = new SpriteMap(GetPath("SkeetDouble"), 41, 7);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(21f, 4f);
            _collisionOffset = new Vec2(-21f, -4f);
            _collisionSize = new Vec2(41f, 7f);
            _fireSound = "shotgunFire";
            _barrelOffsetTL = new Vec2(41f, 0f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireWait = 0.5f;
            _kickForce = 6.55f;
            _editorName = "Skeet Double";
            _holdOffset = new Vec2(9f, 2f);
        }

        public override void Update()
        {
            base.Update();
            _barrelOffsetTL = ammo % 2 == 0 ? new Vec2(41f, 0f) : new Vec2(41f, 2f);
            if (duck != null)
            {
                if (duck.sliding || duck.crouch)
                {
                    if (handAngle > 0f) handAngle -= 0.075f;
                    else if (handAngle < 0f) handAngle += 0.075f;
                    if ((handAngle > -0.075f) & (handAngle < 0.075f)) handAngle = 0f;
                    return;
                }
                if (duck.inputProfile.Down("UP") && !_raised)
                {
                    if (offDir < 0)
                    {
                        if (!_ducklookleft)
                        {
                            handAngle = -handAngle;
                            _ducklookleft = true;
                        }
                        if (handAngle < 0.5f) handAngle += 0.05f;
                    }
                    else
                    {
                        if (_ducklookleft)
                        {
                            handAngle = -handAngle;
                            _ducklookleft = false;
                        }
                        if (handAngle > -0.5f) handAngle -= 0.05f;
                    }

                    return;
                }
            }
            if (handAngle > 0f) handAngle -= 0.075f;
            else if (handAngle < 0f) handAngle += 0.075f;
            if ((handAngle > -0.075f) & (handAngle < 0.075f)) handAngle = 0f;
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