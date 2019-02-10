using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle")]
    public class Nellegalja : Gun, IAmDmr
    {
        public StateBinding LaserSightBinding = new StateBinding(nameof(laserSight));

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
            _graphic = new Sprite(GetPath("Nellegalja"));
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-14.5f, -5f);
            _collisionSize = new Vec2(29f, 11f);
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
			_weight = 4f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                laserSight = !laserSight;
            }
            base.Update();
		}			
	}
}