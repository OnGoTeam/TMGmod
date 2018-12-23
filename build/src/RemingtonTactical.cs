using System;
using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
[EditorGroup("TMG|Shotgun")]
public class RemingtonTac : Gun
{
    private sbyte _loadProgress = 100;

    private float _loadAnimation = 1f;

	//public StateBinding _loadProgressBinding = new StateBinding("_loadProgress");

    private readonly SpriteMap _loaderSprite;

	public RemingtonTac(float xval, float yval)
		: base(xval, yval)
	{
		ammo = 4;
	    _ammoType = new AT9mm
	    {
	        range = 125f,
	        accuracy = 0.69f,
	        penetration = 1f,
	        bulletSpeed = 35f,
	        bulletThickness = 0.5f
	    };
	    _type = "gun";
		graphic = new Sprite(GetPath("RemingtonStock"));
		center = new Vec2(12f, 4f);
		collisionOffset = new Vec2(-12f, -4f);
		collisionSize = new Vec2(24f, 7f);
		_barrelOffsetTL = new Vec2(24f, 1.5f);
        _holdOffset = new Vec2(-1f, 2f);
		_fireSound = "shotgunFire2";
		_kickForce = 2f;
		_numBulletsPerFire = 6;
		_manualLoad = true;
        _fireWait = 1f;
        _loaderSprite = new SpriteMap(GetPath("RemingtonPimp"), 6, 8)
        {
            center = new Vec2(3f, 4f)
        };
        _laserOffsetTL = new Vec2(22f, 0.5f);
        laserSight = true;
        _editorName = "Tactical Remington";
	}

	public override void Update()
	{
		base.Update();
		if (Math.Abs(_loadAnimation - -1f) < 0.01f)
		{
			SFX.Play("shotgunLoad");
			_loadAnimation = 0f;
		}
		if (_loadAnimation >= 0f)
		{
			if (Math.Abs(_loadAnimation - 0.5f) < 0.01f && ammo != 0)
			{
				_ammoType.PopShell(x, y, -offDir);
			}
			if (_loadAnimation < 1f)
			{
				_loadAnimation += 0.125f;
			}
			else
			{
				_loadAnimation = 1f;
			}
		}
		if (_loadProgress >= 0)
		{
			if (_loadProgress == 50)
			{
				Reload(false);
			}
			if (_loadProgress < 100)
			{
				_loadProgress += 10;
			}
			else
			{
				_loadProgress = 100;
			}
		}
	}

	public override void OnPressAction()
	{
		if (loaded)
		{
			base.OnPressAction();
			_loadProgress = -1;
			_loadAnimation = -0.1f;
		}
		else if (_loadProgress == -1)
		{
			_loadProgress = 0;
			_loadAnimation = -1f;
		}
	}

	public override void Draw()
	{
		base.Draw();
		var bOffset = new Vec2(18f, -5f);
		var offset = (float)Math.Sin(_loadAnimation * 3.14f) * 3f;
		base.Draw(_loaderSprite, new Vec2(bOffset.x - 8f - offset, bOffset.y + 4f));
	}
}
}