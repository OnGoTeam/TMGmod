using DuckGame;


namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper")]
    public class SV98 : Sniper
    {
        public SV98(float xval, float yval) : base(xval, yval)
        {
            this.graphic = new Sprite(GetPath("SV99"));
            this.center = new Vec2(13.5f, 4.5f);
            this.collisionOffset = new Vec2(-13.5f, -4.5f);
            this.collisionSize = new Vec2(27f, 9f);
            this._barrelOffsetTL = new Vec2(28f, 5f);
            this.ammo = 6;
            this._ammoType = new AT9mm();
            this._ammoType.penetration = 1f;
            this._ammoType.range = 1000f;
            this._fireSound = GetPath("sounds/Silenced3.wav");
            this._fullAuto = false;
            this._kickForce = 1.25f;
            this.loseAccuracy = 0.5f;
            this.maxAccuracyLost = 1.5f;
            this._holdOffset = new Vec2(1f, 0f);
			this._manualLoad = true;
            this._editorName = "SV-99";
			this.weight = 2f;
		}
        
        public override void Update()
		{
			base.Update();
			if (this._loadState > -1)
			{
				if (this.owner == null)
				{
					if (this._loadState == 2)
					{
						this.loaded = true;
					}
					this._loadState = -1;
					this._angleOffset = 0f;
					this.handOffset = Vec2.Zero;
				}
				if (this._loadState == 0)
				{
					if (Network.isActive)
					{
						if (base.isServerForObject)
						{
							this._netLoad.Play(1f, 0f);
						}
					}
					else
					{
						SFX.Play("loadSniper", 1f, 0f, 0f, false);
					}
					this._loadState++;
				}
				else if (this._loadState == 1)
				{
					if (this._angleOffset < 0.16f)
					{
						this._angleOffset = MathHelper.Lerp(this._angleOffset, 0.25f, 0.25f);
					}
					else
					{
						this._loadState++;
					}
				}
				else if (this._loadState == -1)
				{
					this.handOffset.x = this.handOffset.x + 0.8f;
					if (this.handOffset.x > 4f)
					{
						this._loadState++;
						this.Reload(true);
						this.loaded = false;
					}
				}
				else if (this._loadState == 3)
				{
					this.handOffset.x = this.handOffset.x - 0.8f;
					if (this.handOffset.x <= 0f)
					{
						this._loadState++;
						this.handOffset.x = 0f;
					}
				}
				else if (this._loadState == 4)
				{
					if (this._angleOffset > 0.04f)
					{
						this._angleOffset = MathHelper.Lerp(this._angleOffset, 0f, 0.25f);
					}
					else
					{
						this._loadState = -1;
						this.loaded = true;
						this._angleOffset = 0f;
					}
				}
			}
			if (this.loaded && this.owner != null && this._loadState == -1)
			{
				this.laserSight = false;
				return;
			}
			this.laserSight = false;
		}

		public override void OnPressAction()
		{
            if (this._owner.velocity != new Vec2(0f, 0f))
            {
                this._ammoType.accuracy = 0f;
            }
            else
            {
                this._ammoType.accuracy = 1f;
            }
			if (this.loaded)
			{
				base.OnPressAction();
				return;
			}
			if (this.ammo > 0 && this._loadState == -1)
			{
				this._loadState = 0;
				this._loadAnimation = 0;
			}
		}

		public override void Draw()
		{
			float ang = this.angle;
			if (this.offDir > 0)
			{
				this.angle -= this._angleOffset;
			}
			else
			{
				this.angle += this._angleOffset;
			}
			base.Draw();
			this.angle = ang;
		}

        
	}
}