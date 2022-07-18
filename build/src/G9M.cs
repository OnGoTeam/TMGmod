using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class G9M : BaseLmg, IHaveAllowedSkins, IHaveBipods
    {
        private const int NonSkinFrames = 1;
        private const double Explodechance = 0.005;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly] public int Ammobefore = 71;

        [UsedImplicitly] public StateBinding AmmobeforeBinding = new StateBinding(nameof(Ammobefore));

        [UsedImplicitly] public float Explode;

        [UsedImplicitly] public StateBinding UselessBinding = new StateBinding(nameof(Uselessinteger));

        [UsedImplicitly] public int Uselessinteger = 3;

        public G9M(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 70;
            _ammoType = new ATLowQammos();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("G9M"), 38, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19f, 6f);
            _collisionOffset = new Vec2(-19f, -6f);
            _collisionSize = new Vec2(38f, 11f);
            _barrelOffsetTL = new Vec2(38f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 2.33f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(6f, 1f);
            ShellOffset = new Vec2(-7f, -2f);
            _editorName = "G9M";
            _weight = 6f;
            MaxAccuracy = 0.8f;
            MinAccuracy = 0.7f;
            KickForce1Lmg = 0.23f;
            KickForce2Lmg = 0.43f;
        }

        [UsedImplicitly] public StateBinding ExplodeBinding { get; } = new StateBinding(nameof(Explode));

        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                _kickForce = value ? 0 : 2.33f;
                KickForce1Lmg = value ? 0 : 0.23f;
                KickForce2Lmg = value ? 0 : 0.43f;
                loseAccuracy = value ? 0 : 0.2f;
            }
        }

        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
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

        public override void OnPressAction()
        {
            ammo = Rando.Int(0, Ammobefore / Uselessinteger);
            if (ammo > Ammobefore) ammo = Ammobefore;
            Ammobefore -= ammo;
            if (ammo < 1 && Ammobefore < 1) CreateExplosion(position);
            base.OnPressAction();
        }

        public override void OnReleaseAction()
        {
            if (ammo > 0) Ammobefore += ammo;
            Uselessinteger = 1;
            base.OnReleaseAction();
        }

        public override void Fire()
        {
            Explode = Rando.Float(0, 1);
            if (Explode < Explodechance) CreateExplosion(position);
            base.Fire();
        }

        public override void Thrown()
        {
            if (ammo < 1 && Ammobefore > 0) ammo = Ammobefore;
            base.Thrown();
        }

        private void CreateExplosion(Vec2 pos)
        {
            var cx = pos.x;
            var cy = pos.y - 2f;
            Level.Add(new ExplosionPart(cx, cy));
            var num = 6;
            if (Graphics.effectsLevel < 2) num = 3;
            for (var i = 0; i < num; i++)
            {
                var dir = i * 60f + Rando.Float(-10f, 10f);
                var dist = Rando.Float(12f, 20f);
                var ins = new ExplosionPart(cx + (float)(Math.Cos(Maths.DegToRad(dir)) * dist),
                    cy - (float)(Math.Sin(Maths.DegToRad(dir)) * dist));
                Level.Add(ins);
            }

            SFX.Play("explode");
            Level.Remove(this);
        }
    }
}
