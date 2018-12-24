using System;
using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    public class Ksg12 : BasePumpAction
    {
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
		    _kickForce = 3.75f;
		    _manualLoad = true;
            _fireWait = 1.5f;
            LoaderSprite = new SpriteMap(GetPath("KSG12Pimp"), 19, 9)
            {
                center = new Vec2(3f, 4f)
            };
            _editorName = "KSG-12";
        }
        public override void Draw()
        {
            base.Draw();
            var LoaderVec2 = new Vec2(10f, -2f);
        }
    }
}