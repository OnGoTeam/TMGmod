using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class CZ75 : Gun
    {
		private SpriteMap _sprite;
        private int fdelay = 0;
        public CZ75(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 24;
            this._ammoType = new AT9mm();
            this._ammoType.range = 150f;
            this._ammoType.accuracy = 0.75f;
            this._ammoType.penetration = 0f;
            this._type = "gun";
            this._sprite = new SpriteMap(GetPath("CZ75a"), 12, 8, false);
            this.graphic = this._sprite;
            this.center = new Vec2(8f, 3f);
            this.collisionOffset = new Vec2(-7.5f, -3.5f);
            this.collisionSize = new Vec2(15f, 9f);
            this._barrelOffsetTL = new Vec2(16f, 1f);
            this._fireSound = GetPath("sounds/1.wav");
            this._fullAuto = false;
            this._fireWait = 0.75f;
            this._kickForce = 0f;
            this.loseAccuracy = 0.3f;
            this.maxAccuracyLost = 0.5f;
            this._editorName = "CZ-75";
			this.weight = 1f;
            this._sprite.AddAnimation("haventmagaz", 0.3f, false, new int[]
            {
				1,
			});
            this._sprite.AddAnimation("havemagaz", 0.3f, false, new int[]
            {
				0,
			});
            this._sprite.SetAnimation("havemagaz");
        }

        public override void OnPressAction()
        {
            if (((this.ammo > 0 && this._sprite.currentAnimation == "haventmagaz") || (this.ammo > 12 && this._sprite.currentAnimation == "havemagaz")) && this.fdelay == 0)
            {
                base.Fire();
            }
            else if (this.ammo == 0)
            {
                this.DoAmmoClick();
            }
            else
            {
                if (this.ammo == 12 && this._sprite.currentAnimation == "havemagaz")
                {
                    SFX.Play("click");
                    if (this._raised)
                        Level.Add(new Czmag(this.x, this.y + 1));
                    else if (this.offDir < 0)
                        Level.Add(new Czmag(this.x + 5, this.y));
                    else
                        Level.Add(new Czmag(this.x - 5, this.y));
                    this._sprite.SetAnimation("haventmagaz");
                    this.fdelay = 40;
                }
            }            
        }
        public override void Update()
        {
            base.Update();
            if (this.fdelay > 1)
            {
                this.fdelay -= 1;
            }
            else if (this.fdelay == 1)
            {
                SFX.Play("click");
                this.fdelay -= 1;
            }
        }
    }
}