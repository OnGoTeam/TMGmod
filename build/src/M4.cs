using DuckGame;
using System;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class M4A1 : Gun
    {
        private int _ammobefore = 31;
        private int _counter = 0;
        private int _explodechance;

        public M4A1 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 300f,
                accuracy = 0.8f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("M4A1"));
            center = new Vec2(15f, 6f);
            collisionOffset = new Vec2(-15f, -6f);
            collisionSize = new Vec2(30f, 11f);
            _barrelOffsetTL = new Vec2(31f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.745f;
            _kickForce = 0f;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(3f, 1f);
            _editorName = "M4A1";
			weight = 4.5f;
        }
        public override void OnPressAction()
        {
            ammo = Rando.Int(0,((_ammobefore / 6)*(1+_counter/2)));
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
            _explodechance = Rando.Int(0,2);
            if (_explodechance == 0) CreateExplosion(position);
            base.Fire();
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
            for (int i = 0; i < 25; i++)
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
    }
}