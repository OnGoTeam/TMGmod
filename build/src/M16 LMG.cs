using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using System;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class M16LMG : BaseLmg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        private int _ammobefore = 51;
        private int _counter;
        private float _explode;
        private const double Explodechance = 0.00025;

        public M16LMG (float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 50;
            _ammoType = new ATMagnum
            {
                range = 345f,
                accuracy = 0.8f,
                penetration = 1.5f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("M16LMGpattern"), 38, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19f, 6f);
            _collisionOffset = new Vec2(-19f, -6f);
            _collisionSize = new Vec2(38f, 11f);
            _barrelOffsetTL = new Vec2(38f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.825f;
            _kickForce = 0.33f;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.12f;
            _holdOffset = new Vec2(6f, 1f);
            _editorName = "M16 LMG";
			_weight = 5.75f;
            BaseAccuracy = 0.8f;
            MinAccuracy = 0.7f;
            Kforce1Lmg = 0.23f;
            Kforce2Lmg = 0.43f;
        }
        public override void Update()
        {
            if (duck != null && duck.height < 17f)
            {
                _kickForce = 0f;
				loseAccuracy = 0f;
            }
            else
            {
                _kickForce = 0.33f;
                loseAccuracy = 0.01f;
            }
            base.Update();
        }
        public override void OnPressAction()
        {
            ammo = Rando.Int(0, _ammobefore / 6 * (1 + _counter / 2));
            if (ammo > _ammobefore) ammo = _ammobefore;
            _ammobefore -= ammo;
            base.OnPressAction();
        }
        public override void OnReleaseAction()
        {
            if (ammo > 0) _ammobefore += ammo;
            if (ammo == 0) _counter += 1;
            base.OnReleaseAction();
        }
        public override void Fire()
        {
            _explode = Rando.Float(0, 1);
            if (_explode < Explodechance) CreateExplosion(position);
            base.Fire();
        }
        public override void Thrown()
        {
            if ((ammo < 1) && (_ammobefore > 0)) ammo = _ammobefore;
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
            for (var i = 0; i < 25; i++)
            {
                var dir = i * 18f - 5f + Rando.Float(10f);
                var shrap = new ATShrapnel { range = 20f + Rando.Float(6f) };
                var bullet = new Bullet(x + (float)(Math.Cos(Maths.DegToRad(dir)) * 6.0),
                        y - (float)(Math.Sin(Maths.DegToRad(dir)) * 6.0), shrap, dir)
                { firedFrom = this };
                Level.Add(bullet);
            }
            SFX.Play("explode");
            Level.Remove(this);
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