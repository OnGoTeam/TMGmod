using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using JetBrains.Annotations;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class M16LMG : BaseLmg, IHaveSkin, IHaveBipods
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        //private int _ammobefore = 51;
        //private int _counter;
        //private float _explode;
        //private const double Explodechance = 0;

        public M16LMG (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 50;
            _ammoType = new ATMagnum
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
            Kforce1Lmg = 0.23f;
            Kforce2Lmg = 0.43f;
        }
        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                _kickForce = value ? 0 : 2.33f;
                loseAccuracy = value ? 0 : 0.15f;
            }
        }
        public override void Update()
        {
            Bipods = Bipods;
            base.Update();
        }

        /*public override void OnPressAction()
        {
            /*ammo = Rando.Int(0, _ammobefore / 2 * (1 + _counter / 2));
            if (ammo > _ammobefore) ammo = _ammobefore;
            _ammobefore -= ammo; #1#
            base.OnPressAction();
        }
        public override void OnReleaseAction()
        {
            /*if (ammo > 0) _ammobefore += ammo;
            if (ammo == 0) _counter += 1; #1#
            base.OnReleaseAction();
        }
        public override void Fire()
        {
            /*_explode = Rando.Float(0, 1);
            if (_explode < Explodechance) CreateExplosion(position); #1#
            base.Fire();
        }
        public override void Thrown()
        {
            /*if ((ammo < 1) && (_ammobefore > 0)) ammo = _ammobefore; #1#
            base.Thrown();
        }*/
        /*private void CreateExplosion(Vec2 pos)
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
        }*/
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