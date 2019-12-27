#if DEBUG
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    public class M16A6 : BaseGun, IHaveSkin, IHaveBipods
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 9;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        [UsedImplicitly]
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 3, 6, 7 });
        [UsedImplicitly]
        private uint _cdstate;
        [UsedImplicitly]
        public bool Bipods
        {
            get => BipodsQ(this);
            set => _kickForce = value ? 0 : 5.5f;
        }

        public BitBuffer BipodsBuffer { get; set; }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(Bipods));
        public bool BipodsDisabled => false;

        public M16A6(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 25;
            _ammoType = new ATM16();
            BaseAccuracy = 0.91f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("deleteco/Future/LSTK16v6.png"), 33, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-17f, -7f);
            _collisionSize = new Vec2(33f, 13f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
            {
                center = new Vec2(1.0f, 6f)
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 5.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.45f;
            _holdOffset = new Vec2(3f, 0f);
            ShellOffset = new Vec2(-5f, -2f);
            _editorName = "LSTK-16v6";
            _weight = 6.7f;
        }

        private void UpdCds()
        {
            if (100 < _cdstate && _cdstate < 150) _cdstate = 300;
            if (150 < _cdstate && _cdstate < 200) _cdstate = 0;
            Bipods = Bipods;
        }

        public override void Update()
        {
            if (_cdstate > 0) _cdstate -= 1;
            UpdCds();
            base.Update();
        }

        public override void Fire()
        {
            if ((FrameId + 10) % (10 * NonSkinFrames) >= 20) return;
            UpdCds();
            base.Fire();
            if (Bipods && _wait >= _fireWait) _cdstate += 49;
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
#endif