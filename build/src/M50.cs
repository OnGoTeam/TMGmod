using DuckGame;
using TMGmod.Core;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    public class M50 : Gun
    {
        public M50 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new Cal50Explode
            {
                range = 1100f,
                accuracy = 1f,
                penetration = 1f,
                bulletThickness = 2.5f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("m50"));
            center = new Vec2(20f, 6.5f);
            collisionOffset = new Vec2(-20f, -6.5f);
            collisionSize = new Vec2(40f, 13f);
            _barrelOffsetTL = new Vec2(40f, 6f);
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 3.75f;
            _kickForce = 1.6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(6f, -1f);
            laserSight = true;
            _laserOffsetTL = new Vec2(31f, 10f);
            _editorName = "M50 with Explosive Ammo";
			weight = 6.75f;
        }
    }
}
