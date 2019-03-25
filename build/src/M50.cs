using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    public class M50 : BaseGun, ISpeedAccuracy, IAmSr
    {
        public M50 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 7;
            _ammoType = new Cal50Explode
            {
                range = 1100f,
                accuracy = 1f,
                penetration = 1f,
                bulletThickness = 2.5f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("m50"));
            _center = new Vec2(20f, 6.5f);
            _collisionOffset = new Vec2(-20f, -6.5f);
            _collisionSize = new Vec2(40f, 13f);
            _barrelOffsetTL = new Vec2(40f, 6f);
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 3.75f;
            _kickForce = 1.6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(6f, -1f);
            laserSight = true;
            _laserOffsetTL = new Vec2(31f, 9f);
            _editorName = "M50 with Explosive Ammo";
			_weight = 6.75f;
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.5f;
        }

        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; }
    }
}
