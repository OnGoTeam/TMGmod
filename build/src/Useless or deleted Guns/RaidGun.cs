using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGMod.src
{

    [BaggedProperty("isInDemo", true), BaggedProperty("canSpawn", true)]
    public class RaidGun : Gun
    {

        public RaidGun(float xval, float yval)
            : base(xval, yval)
        {
            this.ammo = 71;
            this._ammoType = new ATShotgun();
            this._ammoType.accuracy = 0.9f;
            this._ammoType.range = 250f;
            this._type = "gun";
            base.graphic = new Sprite(GetPath("RaidGun"), 0f, 0f);
            this.center = new Vec2(12f, 3f);
            this.collisionOffset = new Vec2(-12f, -3f);
            this.collisionSize = new Vec2(24f, 6f);
            this._barrelOffsetTL = new Vec2(23f, 2f);
            this._fireSound = "shotgun";
            this._fullAuto = true;
            this._fireWait = 0.25f;
            this._kickForce = 0.9f;
            this._holdOffset = new Vec2(0f, 2f);
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.3f;
            this._editorName = "RaidGun";
            this._numBulletsPerFire = 5;
            this._ammoType.penetration = 5f;
        }
    }
}