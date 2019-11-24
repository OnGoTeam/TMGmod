using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class RemingtonTac : BasePumpAction
    {
        public RemingtonTac(float xval, float yval) : base(xval, yval)
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
            _numBulletsPerFire = 6;
            _type = "gun";
            _graphic = new Sprite(GetPath("RemingtonStock"));
            _center = new Vec2(12f, 4f);
            _collisionOffset = new Vec2(-12f, -4f);
            _collisionSize = new Vec2(24f, 7f);
            _barrelOffsetTL = new Vec2(24f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(-1f, 2f);
            _fireSound = "shotgunFire2";
            _kickForce = 2.3f;
            _manualLoad = true;
            _fireWait = 1f;
            _laserOffsetTL = new Vec2(22f, 0.5f);
            laserSight = true;
            _editorName = "Tactical Remington";
            LoaderSprite = new SpriteMap(GetPath("RemingtonPimp"), 6, 8)
            {
                center = new Vec2(3f, 4f)
            };
            LoaderVec2 = new Vec2(8f, 0f);
            EpsilonA = 50;
            EpsilonB = 100;
            Loaddx = 2f;
        }
    }
}