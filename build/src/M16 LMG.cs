using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class M16LMG : BaseLmg, IHaveSkin, IHaveBipods
    {
        private const int NonSkinFrames = 1;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public M16LMG(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 50;
            _ammoType = new AT556NATO
            {
                range = 400f,
                accuracy = 0.8f,
                penetration = 1.5f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("M16LMG"), 38, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19f, 6f);
            _collisionOffset = new Vec2(-19f, -6f);
            _collisionSize = new Vec2(38f, 11f);
            _barrelOffsetTL = new Vec2(38f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.83f;
            _kickForce = 2.33f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(6f, 1f);
            ShellOffset = new Vec2(-7f, -2f);
            _editorName = "M16 LMG";
            _weight = 6f;
            BaseAccuracy = 0.8f;
            MinAccuracy = 0.7f;
            KickForce1Lmg = 0.23f;
            KickForce2Lmg = 0.43f;
        }

        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                _kickForce = value ? 0 : 2.33f;
                KickForce1Lmg = value ? 0 : 0.23f;
                KickForce2Lmg = value ? 0 : 0.43f;
                loseAccuracy = value ? 0 : 0.15f;
            }
        }

        [UsedImplicitly]
        public BitBuffer BipodsBuffer
        {
            get
            {
                var b = new BitBuffer();
                b.Write(Bipods);
                return b;
            }
            set => Bipods = value.ReadBool();
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            Bipods = Bipods;
            base.Update();
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
