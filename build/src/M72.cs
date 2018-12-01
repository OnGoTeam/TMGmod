using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Grenadelauncher")]
    public class M72 : Gun
    {

        public M72(float xval, float yval)
            : base(xval, yval)
        {
            this.ammo = 5;
            this._ammoType = new ATGrenade();
            this._ammoType.range = 2000f;
            this._ammoType.accuracy = 0.95f;
            this._ammoType.penetration = 1f;
            this._ammoType.barrelAngleDegrees = -7.5f;
            this._ammoType.bulletSpeed = 10f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("M72a5"));
            this.center = new Vec2(16f, 5.5f);
            this.collisionOffset = new Vec2(-16f, -5.5f);
            this.collisionSize = new Vec2(32f, 11f);
            this._barrelOffsetTL = new Vec2(32f, 4f);
            this._fireSound = "deepMachineGun";
            this._fullAuto = false;
            this._fireWait = 1f;
            this._kickForce = 1.2f;
            this.loseAccuracy = 0.3f;
            this.maxAccuracyLost = 0.5f;
            this._editorName = "M72 Grenade Launcher";
            this.weight = 4.5f;
        }
        public override void Update()
        {
            if (this.ammo == 4) this.graphic = new Sprite(GetPath("M72a4"), 0f, 0f);
            if (this.ammo == 3) this.graphic = new Sprite(GetPath("M72a3"), 0f, 0f);
            if (this.ammo == 2) this.graphic = new Sprite(GetPath("M72a2"), 0f, 0f);
            if (this.ammo == 1) this.graphic = new Sprite(GetPath("M72a1"), 0f, 0f);
            if (this.ammo == 0) this.graphic = new Sprite(GetPath("M72a0"), 0f, 0f);
            base.Update();
        }
    }
}