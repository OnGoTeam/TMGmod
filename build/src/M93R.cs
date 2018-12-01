using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DuckGame;


namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class M93R : Gun
    {

        int _burstNumB = 0;
        int _burstValue;
        float _bw = 5.1f;
		private SpriteMap _sprite;
        public int teksturka = 1;

        public M93R(float xval, float yval)
            : base(xval, yval)
        {
            this._sprite = new SpriteMap((GetPath("M93Rpatterns")), 12, 9, false);
            this.graphic = (Sprite)this._sprite;
            this.teksturka = Rando.Int(0, 4);
            this._sprite.frame = teksturka;
            this.center = new Vec2(6f, 2f);
            this.collisionOffset = new Vec2(-6f, -2f);
            this.collisionSize = new Vec2(12f, 9f);
            this._barrelOffsetTL = new Vec2(13f, 2f);
            this._holdOffset = new Vec2(-2f, 0f);
            this.ammo = 15;
            this._ammoType = new ATMagnum();
            this._fireSound = GetPath("sounds/1.wav");
            this._fullAuto = true;
            this._ammoType.range = 108f;
            this._fireWait = 0.27f;
            this._kickForce = 0.24f;
            this.loseAccuracy = 0.15f;
            this.maxAccuracyLost = 0.2f;
            this._ammoType.range = 110f;
            this._editorName = "M93R";
            this._burstValue = 3;
			this.weight = 2f;
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
                this._bw = 0.3f;
                this._burstNumB = _burstValue;
            }
        }
		public override void Draw()
        {
            this._sprite.frame = teksturka;
            base.Draw();
        }
	}
}