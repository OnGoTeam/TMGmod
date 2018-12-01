using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Shotgun")]
    public class SIX12S : Gun
    {
		
		public EditorProperty<bool> laser = new EditorProperty<bool>(false, null, 0f, 1f, 1f, null, false, false);
		
		public SIX12S (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 6;
            this._ammoType = new AT9mmS();
            this._ammoType.range = 225f;
            this._ammoType.accuracy = 0.9f;
            this._numBulletsPerFire = 14;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("SIX12S"));
            this.center = new Vec2(19.5f, 5f);
            this.collisionOffset = new Vec2(-19.5f, -5f);
            this.collisionSize = new Vec2(29f, 10f);
            this._barrelOffsetTL = new Vec2(30f, 4.5f);
            this._fireSound = "shotgunFire";
            this._fullAuto = false;
            this._fireWait = 1.7f;
            this._kickForce = 1.4f;
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0.5f;
            this.laserSight = false;
            this._laserOffsetTL = new Vec2(24f, 7f);
            this._holdOffset = new Vec2(2f, 0f);
            this._editorName = "SIX12 Silenced";
			this.weight = 4f;
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (this.laser.value == true)
                {
                 this.laserSight = true;
				 this.graphic = new Sprite(GetPath("SIX12Slaser2"));
                }
            }
            base.Initialize();
        }
	}
}