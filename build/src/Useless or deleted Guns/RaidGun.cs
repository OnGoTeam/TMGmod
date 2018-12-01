using DuckGame;

namespace TMGMod.src
{

    [BaggedProperty("isInDemo", true), BaggedProperty("canSpawn", true)]
    public class RaidGun : Gun
    {

        public RaidGun(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 71;
            _ammoType = new ATShotgun
            {
                accuracy = 0.9f,
                range = 250f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("RaidGun"), 0f, 0f);
            center = new Vec2(12f, 3f);
            collisionOffset = new Vec2(-12f, -3f);
            collisionSize = new Vec2(24f, 6f);
            _barrelOffsetTL = new Vec2(23f, 2f);
            _fireSound = "shotgun";
            _fullAuto = true;
            _fireWait = 0.25f;
            _kickForce = 0.9f;
            _holdOffset = new Vec2(0f, 2f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "RaidGun";
            _numBulletsPerFire = 5;
            _ammoType.penetration = 5f;
        }
    }
}