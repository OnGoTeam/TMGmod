using DuckGame;
using TMGmod.Core;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|AutoPistol")]
    public class HazeS : Gun
    {

        public HazeS(float xval, float yval) :
            base(xval, yval)
        {
            ammo = 36;
            _ammoType = new HA
            {
                range = 400f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("haze"));
            center = new Vec2(12f, 3f);
            collisionOffset = new Vec2(-12f, -3f);
            collisionSize = new Vec2(24f, 12f);
            _barrelOffsetTL = new Vec2(25f, 2f);
            _fireSound = GetPath("sounds/SilencedPistol.wav");
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(1f, 0f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.1f;
            _editorName = "AF Haze";
            laserSight = true;
            _laserOffsetTL = new Vec2(16f, 6f);
			weight = 2f;
        }


    }
}
