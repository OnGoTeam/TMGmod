using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [BaggedProperty("canSpawn", false)]
    public class MAPF : Gun
    {
        public MAPF(float xval, float yval)
            : base(xval, yval)
        {
            this.ammo = 20;
            this._ammoType = new ATMagnum();
            this._ammoType.combustable = true;
            this._ammoType.range = 0f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 2f;
            this._numBulletsPerFire = 2;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Mixaly4sPistol2"));
            this.center = new Vec2(7f, 7f);
            this.collisionOffset = new Vec2(-6.5f, -7f);
            this.collisionSize = new Vec2(15f, 12f);
            this._barrelOffsetTL = new Vec2(15f, 3f);
            this._fireSound = "smg";
            this._fullAuto = true;
            this._fireWait = 0.4f;
            this._kickForce = 0.5f;
            this.loseAccuracy = 0.5f;
            this.maxAccuracyLost = 0.8f;
            this._holdOffset = new Vec2(-2f, 3f);
            this._editorName = "FEUERFREI";
			this.weight = 2.5f;
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
        /*public override void OnPressAction()
        {
            base.OnPressAction();
            if (this.ammo > 0)
            {
                this.ammo--;
                SFX.Play("netGunFire", 0.5f, -0.4f + Rando.Float(0.2f), 0f, false);
                base.ApplyKick();
                if (!this.receivingPress && base.isServerForObject)
                {
                    Vec2 pos = this.Offset(base.barrelOffset);
                    MF d = new MF(pos.x, pos.y, this, 8);
                    base.Fondle(d);
                    Vec2 travelDir = Maths.AngleToVec(base.barrelAngle);
                    d.hSpeed = travelDir.x * 14f;
                    d.vSpeed = travelDir.y * 14f;
                    Level.Add(d);
                    return;
                }
            }
            else
            {
                base.DoAmmoClick();
            }

        }*/

        public override void Fire()
        {
            if (this.ammo > 0)
            {
                //this.ammo--;
                base.ApplyKick();
                if (!this.receivingPress && base.isServerForObject)
                {
                    Vec2 pos = this.Offset(base.barrelOffset);
                    MF d = new MF(pos.x, pos.y, this, 8);
                    base.Fondle(d);
                    Vec2 travelDir = Maths.AngleToVec(base.barrelAngle);
                    d.hSpeed = travelDir.x * 14f;
                    d.vSpeed = travelDir.y * 14f;
                    Level.Add(d);
                }
            }
            base.Fire();
        }


    }
}
