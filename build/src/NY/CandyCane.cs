using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    [UsedImplicitly]
    public class CandyCane:Gun
    {
        private bool _dropped;
        public CandyCane(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            _graphic = new Sprite(GetPath("Holiday/candycane"));
            _ammoType = new ATCane(this)
            {
                range = 500f,
                accuracy = 0.95f
            };
            _type = "gun";
            _center = new Vec2(9f, 3.5f);
            _collisionOffset = new Vec2(-9f, -3.5f);
            _collisionSize = new Vec2(18f, 7f);
            _barrelOffsetTL = new Vec2(18f, 3.5f);
            _fireSound = "woodHit";
            _fullAuto = false;
            _fireWait = 1.2f;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(-1f, 1f);
            _editorName = "CandyCane";
            _weight = 2.5f;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
        }

        /// <inheritdoc />
        public override void Reload(bool shell = true)
        {
            if (loaded) return;
            duck?.ThrowItem(false);
            Level.Remove(this);
        }

        public virtual void Drop(float x1, float y1, bool force=false, float p=0.75f)
        {
            if (_dropped) return;
            _dropped = true;
            if (!force && !(Rando.Float(1) < p)) return;
            //else
            var c = new CandyCane(x1, y1);
            Level.Add(c);
        }
    }
}