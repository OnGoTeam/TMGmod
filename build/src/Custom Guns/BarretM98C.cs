using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DuckGame;


namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper|Custom")]
    public class BarretM98C : Sniper
    {
		
        public BarretM98C(float xval, float yval) : base(xval, yval)
        {
            this.graphic = new Sprite(GetPath("BarretM98short"));
            this.center = new Vec2(17f, 10f);
            this.collisionOffset = new Vec2(-25f, -10f);
            this.collisionSize = new Vec2(50f, 13f);
            this._barrelOffsetTL = new Vec2(39f, 6f);
            this._ammoType.accuracy = 0.9f;
            this.ammo = 8;
            this._ammoType = new ATSniper();
            this._fireSound = GetPath("sounds/HeavySniper.wav");
            this._fullAuto = false;
            this._kickForce = 2.5f;
            this.laserSight = false;
            this._laserOffsetTL = new Vec2(31f, 9f);
            this._holdOffset = new Vec2(-4f, 2f);
            this._editorName = "Barrett M98C";
			this.weight = 6.5f;
			

        }

        public override void Draw()
        {
            float ang = this.angle;
            if (this.offDir <= 0)
            {
                this.angle = this.angle + this._angleOffset;
            }
            else
            {
                this.angle = this.angle - this._angleOffset;
            }
            base.Draw();
            this.angle = ang;
            this.laserSight = false;
        }

        public override void OnPressAction()
        {
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

        public override void Update()
        {
            base.Update();
            if (this._loadState > -1)
            {
                if (this.owner == null)
                {
                    if (this._loadState == 3)
                    {
                        this.loaded = true;
                    }
                    this._loadState = -1;
                    this._angleOffset = 0f;
                    this.handOffset = Vec2.Zero;
                }
                if (this._loadState == 0)
                {
                    if (!Network.isActive)
                    {
                        SFX.Play("loadSniper", 1f, 0f, 0f, false);
                    }
                    else if (base.isServerForObject)
                    {
                        this._netLoad.Play(1f, 0f);
                    }
                    Sniper sniper = this;
                    sniper._loadState = sniper._loadState + 1;
                }
                else if (this._loadState == 1)
                {
                    if (this._angleOffset >= 0.1f)
                    {
                        Sniper sniper1 = this;
                        sniper1._loadState = sniper1._loadState + 1;
                    }
                    else
                    {
                        this._angleOffset = this._angleOffset + 0.003f;
                    }
                }
                else if (this._loadState == 2)
                {
                    this.handOffset.x = this.handOffset.x - 0.2f;
                    if (this.handOffset.x > 4f)
                    {
                        Sniper sniper2 = this;
                        sniper2._loadState = sniper2._loadState + 1;
                        this.Reload(true);
                        this.loaded = false;
                    }
                }
                else if (this._loadState == 3)
                {
                    this.handOffset.x = this.handOffset.x + 0.2f;
                    if (this.handOffset.x <= 0f)
                    {
                        Sniper sniper3 = this;
                        sniper3._loadState = sniper3._loadState + 1;
                        this.handOffset.x = 0f;
                    }
                }
                else if (this._loadState == 4)
                {
                    if (this._angleOffset <= 0.03f)
                    {
                        this._loadState = -1;
                        this.loaded = true;
                        this._angleOffset = 0f;
                    }
                    else
                    {
                        this._angleOffset = MathHelper.Lerp(this._angleOffset, 0f, 0.15f);
                    }
                }
            }
            this.laserSight = false;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
	}
}