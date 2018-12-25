using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Rifle")]
    public class Nellegalja : Gun
    {
        private bool _laser;
        private bool _changed;

        public Nellegalja (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 15;
            _ammoType = new AT9mmS
            {
                range = 624f,
                accuracy = 0.91f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("Nellegalja"));
            center = new Vec2(15f, 5f);
            collisionOffset = new Vec2(-14.5f, -5f);
            collisionSize = new Vec2(29f, 11f);
            _barrelOffsetTL = new Vec2(30f, 4f);
            _fireSound = GetPath("sounds/Silenced2.wav");
            _fullAuto = false;
            _fireWait = 0.88f;
            _kickForce = 0.9f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            laserSight = true;
            _laserOffsetTL = new Vec2(21f, 1.5f);
            _editorName = "Nellegalja Weapon";
			weight = 4f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (duck.inputProfile.Down("QUACK"))
                {
                    if (!_changed)
                    {
                        if (_laser)
                        {
                            laserSight = true;
                            _laser = false;
                        }
                        else
                        {
                            laserSight = false;
                            _laser = true;
                        }

                        _changed = true;
                    }
                }
                else
                {
                    _changed = false;
                }
			}
		    base.Update();
		}			
	}
}