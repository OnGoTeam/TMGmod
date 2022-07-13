using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AF2011 : BaseGun, IAmHg, IHaveSkin, I5
    {
        private const int NonSkinFrames = 1;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 3, 5 });

        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public AF2011(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 10;
            _ammoType = new ATAF2011();
            _numBulletsPerFire = 2;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("AF2011"), 16, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-8f, -4f);
            _collisionSize = new Vec2(16f, 9f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _fireSound = "pistolFire";
            _fullAuto = false;
            _fireWait = 0.6f;
            _kickForce = 1.7f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.4f;
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "AF-2011";
            _weight = 2.5f;
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

        public override void Fire()
        {
            if (ammo > 0) _ammoType.accuracy -= 0.05f;
            base.Fire();
        }

        public override void Update()
        {
            if (_ammoType.accuracy + 0.01f < 0.95f)
                _ammoType.accuracy += 0.003f;
            else
                _ammoType.accuracy = 0.95f;
            base.Update();
        }

        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell)
                {
                    _ammoType.PopShell(x, y, -offDir);
                    _ammoType.PopShell(x, y, -offDir);
                }

                --ammo;
            }

            loaded = true;
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
