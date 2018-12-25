using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
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
            graphic = new Sprite(GetPath("RemingtonStock"));
            center = new Vec2(12f, 4f);
            collisionOffset = new Vec2(-12f, -4f);
            collisionSize = new Vec2(24f, 7f);
            _barrelOffsetTL = new Vec2(24f, 1.5f);
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
            LoaderVec2 = new Vec2(2f, 1f);
            EpsilonA = 50;
            EpsilonB = 100;
            Loaddx = 2f;
        }
    }
}