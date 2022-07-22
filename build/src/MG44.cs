﻿using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG44 : BaseLmg, IHaveAllowedSkins, IHaveBipods
    {
        public MG44(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 60;
            _ammoType = new ATMG44();
            MaxAccuracy = 0.75f;
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("MG44 Mark2H"), 39, 11);
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

        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 6, 7 });

        public bool Bipods
        {
            get => HandleQ();
            set
            {
                KickForce1Lmg = value ? .9f : 1.8f;
                KickForce2Lmg = value ? 1.5f : 1.8f;
                loseAccuracy = value ? 0f : 0.1f;
                maxAccuracyLost = value ? 0f : 0.3f;
            }
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;

        public override void Update()
        {
            base.Update();
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (ammo)
            {
                case 1:
                    NonSkin = 1;
                    break;
                case 0:
                    NonSkin = 2;
                    break;
            }
        }

        protected override void PopBaseShell()
        {
            ATMG44.PopShellSkin(Offset(ShellOffset).x, Offset(ShellOffset).y, FrameId, AddShell);
        }
    }
}
