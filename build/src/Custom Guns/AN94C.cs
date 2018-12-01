using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DuckGame;


namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun|Custom")]
    public class AN94C : Gun
    {

        int _burstNumB = 0;
        int _burstValue;
        float _bw = 5.1f;
		
		public EditorProperty<bool> laser = new EditorProperty<bool>(false, null, 0f, 1f, 1f, null, false, false);

        public AN94C(float xval, float yval)
            : base(xval, yval)
        {
            this.graphic = new Sprite(GetPath("AN94notfol"));
            this.center = new Vec2(16f, 5f);
            this.collisionOffset = new Vec2(-15f, -6f);
            this.collisionSize = new Vec2(33f, 9f);
            this._barrelOffsetTL = new Vec2(34f, 3f);
            this._holdOffset = new Vec2(2f, 2f);
            this.ammo = 30;
            this._ammoType = new ATMagnum();
            this._fireSound = "deepMachineGun2";
            this._fullAuto = true;
            this._fireWait = 0.1f;
            this._kickForce = 0.9f;
            this.loseAccuracy = 0.15f;
            this.maxAccuracyLost = 0.1f;
            this._ammoType.range = 310f;
            this._editorName = "AN94 Fixed Stock";
            this._burstValue = 2;
			this.weight = 5f;
            this._laserOffsetTL = new Vec2(30f, 2.5f);
        }

        public override void Fire()
        {
           // base.Fire();
        }
        public override void Update()
        {
            //object obj;

            if (this._burstNumB > 0 && this._bw > 0.1f)
            {
                base.Fire();
                this._burstNumB = _burstNumB -1;
                this._bw = 0;
            }
            else
            {
                this._bw = this._bw + 0.1f;
            }
            base.Update();
        }
        public override void OnPressAction()
        {
            if (this._bw > 1f)
            {
                this._bw = 0.2f;
                this._burstNumB = _burstValue;
            }
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (this.laser.value == true)
                {
                 this.laserSight = true;
                }
            }
            base.Initialize();
        }
	}
}