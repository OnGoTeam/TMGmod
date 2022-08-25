using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class G9M : BaseLmg, IHaveAllowedSkins
    {
        private const double Explodechance = 0.005;

        public int Ammobefore = 71;
        [UsedImplicitly] public StateBinding AmmobeforeBinding = new StateBinding(nameof(Ammobefore));
        public float Explode;
        [UsedImplicitly] public StateBinding UselessBinding = new StateBinding(nameof(Uselessinteger));
        public int Uselessinteger = 3;

        public G9M(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "G9M";
            ammo = 70;
            SetAmmoType<ATLowQammos>();
            
            Smap = new SpriteMap(GetPath("G9M"), 38, 11);
            _center = new Vec2(19f, 6f);
            _collisionOffset = new Vec2(-19f, -6f);
            _collisionSize = new Vec2(38f, 11f);
            _barrelOffsetTL = new Vec2(38f, 3.5f);
            _flare = FrameUtils.FlareOnePixel2();
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 2.33f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(5f, 1f);
            ShellOffset = new Vec2(-6f, -2f);
            _weight = 6f;
            MinAccuracy = 0.7f;
            KickForce1Lmg = 0.23f;
            KickForce2Lmg = 0.43f;
        }

        [UsedImplicitly] public StateBinding ExplodeBinding { get; } = new StateBinding(nameof(Explode));

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 8 });

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

        protected override void RealFire()
        {
            Explode = Rando.Float(0, 1);
            if (Explode < Explodechance) CreateExplosion(position);
            base.RealFire();
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

        public override void Update()
        {
            _kickForce = BipodsQ() ? 0 : 2.33f;
            KickForce1Lmg = BipodsQ() ? 0 : 0.23f;
            KickForce2Lmg = BipodsQ() ? 0 : 0.43f;
            loseAccuracy = BipodsQ() ? 0 : 0.2f;
            base.Update();
        }
    }
}
