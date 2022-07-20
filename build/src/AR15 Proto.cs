using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AR15Proto : BaseAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private const double Explodechance = 0.006;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly] public int Ammobefore = 21;

        [UsedImplicitly] public StateBinding AmmobeforeBinding = new StateBinding(nameof(Ammobefore));

        [UsedImplicitly] public float Explode;

        [UsedImplicitly] public StateBinding UselessBinding = new StateBinding(nameof(Uselessinteger));

        [UsedImplicitly] public int Uselessinteger = 3;

        public AR15Proto(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new ATLowQammos();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("AR15Proto"), 27, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19f, 5f);
            _collisionOffset = new Vec2(-19f, -5f);
            _collisionSize = new Vec2(27f, 10f);
            _barrelOffsetTL = new Vec2(27f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.75f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.21f;
            _holdOffset = new Vec2(5f, 0f);
            ShellOffset = new Vec2(-7f, -1f);
            _editorName = "AR15 Proto";
            _weight = 4.2f;
            _kickForce = 0.07f;
            KforceDelta = 0.43f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 330f;
            base.OnInitialize();
        }

        [UsedImplicitly] public StateBinding ExplodeBinding { get; } = new StateBinding(nameof(Explode));

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

            /*
            for (var i = 0; i < 25; i++)
            {
                var dir = i * 18f - 5f + Rando.Float(10f);
                var shrap = new ATShrapnel { range = 20f + Rando.Float(6f) };
                var bullet = new Bullet(x + (float)(Math.Cos(Maths.DegToRad(dir)) * 6.0),
                        y - (float)(Math.Sin(Maths.DegToRad(dir)) * 6.0), shrap, dir)
                { firedFrom = this };
                Level.Add(bullet);
            }
            */
            SFX.Play("explode");
            Level.Remove(this);
        }
    }
}
