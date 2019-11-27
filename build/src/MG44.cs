using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG44 : BaseGun, IHaveSkin, IAmLmg, IHaveBipods
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        public float RandomaticKickforce;
        [UsedImplicitly]
        public StateBinding RandomaticKickforceBinding { get; } = new StateBinding(nameof(RandomaticKickforce));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 3, 6, 7 });
        public MG44(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 60;
            _ammoType = new ATMG44();
            BaseAccuracy = 0.75f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MG44 Mark2H"), 39, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20f, 6f);
            _collisionOffset = new Vec2(-20f, -6f);
            _collisionSize = new Vec2(39f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _flare = new SpriteMap(GetPath("FlareMG44"), 13, 10)
            {
                center = new Vec2(1.0f, 6f)
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 1.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(5f, 1f);
            ShellOffset = new Vec2(-5f, -3f);
            _editorName = "MG44 Mark2H";
            _weight = 7.5f;
        }
        public override void Update()
        {
            base.Update();
            switch (ammo)
            {
                case 1:
                    if (_sprite.frame < 10) _sprite.frame += 10;
                    break;
                case 0:
                    if (_sprite.frame < 20) _sprite.frame += 10;
                    break;
            }
            Bipods = Bipods;
            RandomaticKickforce = Rando.Float(0.9f, 1.5f);
        }
        public bool Bipods
        {
            get => HandleQ();
            set
            {
                _kickForce = value ? RandomaticKickforce : 1.8f;
                loseAccuracy = value ? 0f : 0.1f;
                maxAccuracyLost = value ? 0f : 0.3f;
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
        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell)
                {
                    ATMG44.PopShell(Offset(ShellOffset).x, Offset(ShellOffset).y, -offDir, FrameId);
                }
                --ammo;
            }
            loaded = true;
        }
    }
}