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
    public class MG44 : BaseLmg, IHaveAllowedSkins, IHaveBipods
    {
        private const int NonSkinFrames = 3;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 6, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly] public float RandomaticKickforce;

        public MG44(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 60;
            _ammoType = new ATMG44();
            MaxAccuracy = 0.75f;
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
                center = new Vec2(1.0f, 6f),
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

        [UsedImplicitly]
        public StateBinding RandomaticKickforceBinding { get; } = new StateBinding(nameof(RandomaticKickforce));

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
            base.Update();
            // ReSharper disable once SwitchStatementMissingSomeCases
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

        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell) ATMG44.PopShellSkin(Offset(ShellOffset).x, Offset(ShellOffset).y, FrameId, AddShell);
                --ammo;
            }

            loaded = true;
        }
    }
}
