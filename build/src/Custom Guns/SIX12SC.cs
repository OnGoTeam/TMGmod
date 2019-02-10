using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Shotgun|Custom")]
    // ReSharper disable once InconsistentNaming
    public class SIX12SC : Gun, IAmSg
    {

        public SIX12SC(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new AT9mmS
            {
                range = 225f,
                accuracy = 0.9f,
                penetration = 1f
            };
            _numBulletsPerFire = 14;
            _type = "gun";
            _graphic = new Sprite(GetPath("SIX12Slaser2"));
            _center = new Vec2(19.5f, 5f);
            _collisionOffset = new Vec2(-19.5f, -5f);
            _collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 1.7f;
            _kickForce = 5f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            laserSight = true;
            _laserOffsetTL = new Vec2(24f, 7f);
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SIX12S with Laser";
            _weight = 4f;
        }
    }
}