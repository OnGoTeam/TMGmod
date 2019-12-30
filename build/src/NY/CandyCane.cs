using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    [UsedImplicitly]
    public class CandyCane:Gun
    {
        [UsedImplicitly]
        public bool Dropped;
        [UsedImplicitly]
        public StateBinding DroppedBinding = new StateBinding(nameof(Dropped));
        public CandyCane(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            _graphic = new Sprite(GetPath("Holiday/candycane"));
            _ammoType = new ATCane
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
        }

        public override void Reload(bool shell = true)
        {
            if (ammo > 0)
            {
                --ammo;
            }
            loaded = true;
        }

        public override void Update()
        {
            base.Update();
            if (ammo > 0 || !loaded) return;
            //else
            duck?.ThrowItem(false);
            Level.Remove(this);
        }

        public virtual void Drop(Vec2 pos, bool force=false, float p=0.75f)
        {
            if (Dropped) return;
            Dropped = true;
            if (!force && !(Rando.Float(1) < p)) return;
            //else
            var ctor = GetType().GetConstructor(new[] {typeof(float), typeof(float)});
            if (ctor == null) return;
            //else
            if (!(ctor.Invoke(new object[] {pos.x, pos.y}) is Thing t)) return;
            //else
            Level.Add(t);
        }
    }
}