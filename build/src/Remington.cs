using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
[EditorGroup("TMG|Shotgun")]
public class Remington : Gun
{
	public sbyte _loadProgress = 100;

	public float _loadAnimation = 1f;

	public StateBinding _loadProgressBinding = new StateBinding("_loadProgress", -1, false, false);

	protected SpriteMap _loaderSprite;

	public Remington(float xval, float yval)
		: base(xval, yval)
	{
		this.ammo = 3;
		this._ammoType = new AT9mm();
        this._ammoType.range = 115f;
        this._ammoType.accuracy = 0.57f;
        this._ammoType.penetration = 1f;
		this._type = "gun";
		this.graphic = new Sprite(GetPath("Remington"));
		this.center = new Vec2(12f, 4f);
		this.collisionOffset = new Vec2(-12f, -4f);
		this.collisionSize = new Vec2(24f, 7f);
		this._barrelOffsetTL = new Vec2(24f, 1.5f);
        this._holdOffset = new Vec2(-1f, 2f);
		this._fireSound = "shotgunFire2";
		this._kickForce = 2.5f;
		this._numBulletsPerFire = 5;
        this._ammoType.bulletSpeed = 19f;
        this._ammoType.bulletThickness = 0.6f;
		this._manualLoad = true;
        this._fireWait = 5f;
		this._loaderSprite = new SpriteMap((GetPath("RemingtonPimp")), 6, 8, false);
		this._loaderSprite.center = new Vec2(3f, 4f);
        this._editorName = "Remington";
	}

	public override void Update()
	{
		base.Update();
		if (this._loadAnimation == -1f)
		{
			SFX.Play("shotgunLoad", 1f, 0f, 0f, false);
			this._loadAnimation = 0f;
		}
		if (this._loadAnimation >= 0f)
		{
			if (this._loadAnimation == 0.5f && base.ammo != 0)
			{
				base._ammoType.PopShell(base.x, base.y, -this.offDir);
			}
			if (this._loadAnimation < 1f)
			{
				this._loadAnimation += 0.0625f;
			}
			else
			{
				this._loadAnimation = 1f;
			}
		}
		if (this._loadProgress >= 0)
		{
			if (this._loadProgress == 50)
			{
				this.Reload(false);
			}
			if (this._loadProgress < 100)
			{
				this._loadProgress += 10;
			}
			else
			{
				this._loadProgress = 100;
			}
		}
	}

	public override void OnPressAction()
	{
		if (base.loaded)
		{
			base.OnPressAction();
			this._loadProgress = -1;
			this._loadAnimation = -0.2f;
		}
		else if (this._loadProgress == -1)
		{
			this._loadProgress = 0;
			this._loadAnimation = -1f;
		}
	}

	public override void Draw()
	{
		base.Draw();
		Vec2 bOffset = new Vec2(18f, -5f);
		float offset = (float)Math.Sin((double)(this._loadAnimation * 3.14f)) * 3f;
		base.Draw(this._loaderSprite, new Vec2(bOffset.x - 8f - offset, bOffset.y + 4f), 1);
	}
}
}