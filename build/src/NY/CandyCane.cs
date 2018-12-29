using DuckGame;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc")]
    public class CandyCane:Gun
    {
        public CandyCane(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            _ammoType = new ATCandyCane
            {
                sprite = new Sprite(GetPath("Holiday/candycane"))
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("Holiday/candycane"));
            _center = new Vec2(9f, 3.5f);
            _collisionOffset = new Vec2(-9f, -3.5f);
            _collisionSize = new Vec2(18f, 7f);
            _barrelOffsetTL = new Vec2(18f, 3.5f);
            _fireSound = "woodHit";
            _fullAuto = false;
            _fireWait = 1.2f;
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(-1f, 1f);
            _editorName = "CandyCane";
            _weight = 2.5f;
        }

        public override void Reload(bool shell = true)
        {
            if (loaded) return;
            duck?.ThrowItem(false);
            Level.Remove(this);
        }
    }
}