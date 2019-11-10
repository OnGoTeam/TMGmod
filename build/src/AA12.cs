using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Fully-Automatic")]
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class AA12 : BaseGun, IAmSg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7, 8, 9 });

        public AA12 (float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 12;
            _ammoType = new ATMagnum
            {
                range = 125f,
                accuracy = 0.4f,
                penetration = 1f,
                bulletThickness = 1.5f
            };
            BaseAccuracy = 0.8f;
            _numBulletsPerFire = 12;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("AA12"), 34, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-17f, -7f);
            _collisionSize = new Vec2(34f, 13f);
            _barrelOffsetTL = new Vec2(34f, 4f);
            _fireSound = "shotgunFire";
            _fullAuto = true;
            _fireWait = 1.2f;
            _kickForce = 6f;
            loseAccuracy = 0.35f;
            maxAccuracyLost = 0.8f;
            _holdOffset = new Vec2(1f, 0f);
            ShellOffset = new Vec2(-6f, -1f);
            _editorName = "AA-12";
			_weight = 7f;
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

        public override void Fire()
        {
            if (duck?.sliding == true) _accuracyLost = 0;
            base.Fire();
            ApplyKick();
            if ((owner as Duck)?.ragdoll != null) return;
            if (owner == null) return;
            if (duck == null) return;
            if (!duck.sliding) return;
            if (!duck.grounded) return;
            owner.vSpeed = 0f;
        }
    }
}