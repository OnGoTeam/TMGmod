using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    // ReSharper disable once InconsistentNaming
    public class PPLMG : BaseLmg
    {
        public PPLMG (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 70;
            _ammoType = new ATCane(_graphic = new SpriteMap(GetPath("Holiday/candycane"), 18, 7));
            _type = "gun";
            _graphic = new SpriteMap(GetPath("Holiday/Peppergun"), 18, 7);
            _center = new Vec2(9f, 4f);
            _collisionOffset = new Vec2(-9f, -4f);
            _collisionSize = new Vec2(18f, 7f);
            _barrelOffsetTL = new Vec2(18f, 0f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.83f;
            _kickForce = 0.33f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(-2f, 0f);
            _editorName = "Peppermint Machinegun";
			_weight = 2f;
        }
    }
}