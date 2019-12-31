using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Break-Action")]
    public class Deadly44C : BaseGun, IAmSg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });

        public Deadly44C (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 2;
            _ammoType = new AT44DB();
            BaseAccuracy = 0.1f;
            _numBulletsPerFire = 44;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("44dbTWICE"), 33, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16.5f, 5f);
            _collisionOffset = new Vec2(-16.5f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(33f, 2f);
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-6f, -7f);
            _fireSound = "shotgun";
            _manualLoad = true;
            _fireWait = 4f;
            _kickForce = 9f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.5f;
            _editorName = "You Scared Ded Twice";
			_weight = 4.25f;
        }
        public override void OnPressAction()
        {
            if (!loaded && ammo > 1) SFX.Play(GetPath("sounds/tuduc.wav"));
            if (!loaded && ammo > 0)
            {
                _sprite.frame = _sprite.frame % 10 + 10;
                Reload();
            }
            else if (loaded)
            {
                Fire();
            }
        }
        public override void Reload(bool shell = true)
        {
            base.Reload(ammo > 1);
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