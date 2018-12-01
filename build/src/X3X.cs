using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class X3X : Gun
    {
        private SpriteMap _sprite;
        public X3X (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 8;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 500f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 100f;
            this._ammoType.bulletThickness = 5f;
            this._type = "gun";
            //this.graphic = new Sprite(GetPath("X3X"));
            this._sprite = new SpriteMap(GetPath("X3Xa"), 28, 15, false);
            this.graphic = this._sprite;
            this.center = new Vec2(14f, 9f);
            this.collisionOffset = new Vec2(-11.5f, -9f);
            this.collisionSize = new Vec2(23f, 15f);
            this._barrelOffsetTL = new Vec2(28f, 5f);
            this._fireSound = "deepMachineGun2";
            this._fullAuto = false;
            this._fireWait = 2.5f;
            this._kickForce = 10f;
            this.loseAccuracy = 1.9f;
            this.maxAccuracyLost = 0.5f;
            this._holdOffset = new Vec2(0f, 2f);
            this._editorName = "EXsess's X3X";
			this.weight = 5.5f;
            this._manualLoad = true;
            this._sprite.AddAnimation("idle", 0.3f, false, new int[]
            {
				0,
			});
            this._sprite.AddAnimation("fire", 0.3f, false, new int[]
            {
				1,
			});
            this._sprite.AddAnimation("empty", 1f, true, new int[]
            {
                1
            });
        }

        public override void Fire()
        {
            //base.Fire();
        }
        public override void Update()
        {
            if (this._sprite.currentAnimation == "fire" && this._sprite.finished)
            {
                this._sprite.SetAnimation("idle");
            }
            base.Update();
        }

        public override void OnPressAction()
        {
            if (this.ammo == 0)
            {
                this._sprite.SetAnimation("empty");
            }
            if (this.loaded)
            {
                base.Fire();
            }
            else
            {
                SFX.Play("Click");
                this._sprite.SetAnimation("fire");
                base.Reload();
            }
            base.OnPressAction();
        }
        
    }
}
