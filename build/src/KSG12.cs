using System;
using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    public class Ksg12 : Gun
    {
        private sbyte _loadProgress = 100;
        private float _loadAnimation = 1f;
        private readonly SpriteMap _loaderSprite;

	    public Ksg12(float xval, float yval)
		    : base(xval, yval)
	    {
		    ammo = 15;
	        _ammoType = new AT9mm
	        {
	            range = 185f,
	            accuracy = 0.35f,
	            penetration = 1f,
	            bulletSpeed = 50f,
	            bulletThickness = 0.25f
	        };
            _numBulletsPerFire = 4;
            _type = "gun";
		    graphic = new Sprite(GetPath("KSG12"));
		    center = new Vec2(18f, 5.5f);
		    collisionOffset = new Vec2(-18f, -5.5f);
		    collisionSize = new Vec2(36f, 11f);
		    _barrelOffsetTL = new Vec2(36f, 3f);
            _holdOffset = new Vec2(-1f, 0f);
		    _fireSound = "shotgunFire2";
		    _kickForce = 2f;
		    _manualLoad = true;
            _fireWait = 1.5f;
            _loaderSprite = new SpriteMap(GetPath("KSG12Pimp"), 19, 9)
            {
                center = new Vec2(3f, 4f)
            };
            _editorName = "KSG-12";
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
				    _loadProgress += 5;
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
		    var bOffset = new Vec2(10f, -4.5f);
		    var offset = (float)Math.Sin(_loadAnimation * 3.14f) * 3f;
		    Draw(_loaderSprite, new Vec2(bOffset.x - 8f - offset, bOffset.y + 4f));
	    }
    }
}