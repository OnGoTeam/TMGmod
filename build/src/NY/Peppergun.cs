using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class PPLMG : BaseLmg
    {
        public PPLMG(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 7;
            _numBulletsPerFire = 12;
            _ammoType = new ATCane
            {
                range = 100f,
                accuracy = 0.11f,
            };
            _type = "gun";
            SkinFrames = 1;
            Smap = new SpriteMap(GetPath("Holiday/Peppergun"), 18, 7);
            _center = new Vec2(9f, 4f);
            _collisionOffset = new Vec2(-9f, -4f);
            _collisionSize = new Vec2(18f, 7f);
            _barrelOffsetTL = new Vec2(18f, 0f);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4)
            {
                center = new Vec2(0f, 0f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.6f;
            _kickForce = 0.33f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(-8f, 3f);
            _editorName = "Big Sweet Gun";
            _weight = 2f;
        }
    }
}
