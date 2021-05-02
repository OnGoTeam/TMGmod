#if DEBUG
using System;
using DuckGame;
using TMGmod.Useless_or_deleted_Guns;


namespace TMGmod.Core
{
    [Obsolete]
    // ReSharper disable once InconsistentNaming
    public class MF : PhysicsObject, IPlatform
    {
        private readonly SpriteMap _sprite;

        private new readonly MAPF _owner;

        private readonly int _numFlames;

        public MF(float xpos, float ypos, MAPF owner, int numFlames = 8)
            : base(xpos, ypos)
        {
            _sprite = new SpriteMap("smallFire", 16, 16);
            _sprite.AddAnimation("burn", 0.2f + Rando.Float(0.2f), true, 0, 1, 2, 3, 4);
            _sprite.SetAnimation("burn");
            _sprite.imageIndex = Rando.Int(4);
            _graphic = _sprite;
            _center = new Vec2(8f, 8f);
            _collisionOffset = new Vec2(-4f, -2f);
            _collisionSize = new Vec2(8f, 4f);
            depth = -0.5f;
            thickness = 1f;
            _weight = 1f;
            breakForce = 9999999f;
            _owner = owner;
            _weight = 0.5f;
            gravMultiplier = 0.7f;
            _numFlames = numFlames;
        }

        protected override bool OnDestroy(DestroyType dtype = null)
        {
            if (isServerForObject)
            {
                for (var i = 0; i < _numFlames; i++)
                {
                    Level.Add(SmallFire.New(x - hSpeed, y - vSpeed, -3f + Rando.Float(6f), -3f + Rando.Float(6f), false, null, true, this));
                }
            }
            SFX.Play("flameExplode", 0.9f, -0.1f + Rando.Float(0.2f));
            Level.Remove(this);
            return true;
        }

        public override void Update()
        {
            if (Rando.Float(2f) < 0.3f) vSpeed += -2f + Rando.Float(3.5f);
            if (Rando.Float(9f) < 0.1f) vSpeed += -3f + Rando.Float(3.1f);
            if (Rando.Float(14f) < 0.1f) vSpeed += -5f + Rando.Float(4f);
            if (Rando.Float(25f) < 0.1f) vSpeed += -7f + Rando.Float(6f);
            Level.Add(SmallSmoke.New(x, y));
            if (hSpeed > 0f)
                _sprite.angleDegrees = 90f;
            else if (hSpeed < 0f) _sprite.angleDegrees = -90f;

            base.Update();
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            if (!isServerForObject || with == _owner || with is Gun || !(with.weight >= 5f)) return;
            //ese
            if (with is PhysicsObject)
            {
                with.hSpeed = hSpeed / 4f;
                with.vSpeed -= 1f;
            }
            Destroy(new DTImpact(null));
            with.Burn(position, this);
        }
    }
}
#endif