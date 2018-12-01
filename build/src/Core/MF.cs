using DuckGame;

namespace TMGmod.src
{
    public class MF : PhysicsObject, IPlatform
    {
        private SpriteMap _sprite;

        private new MAPF _owner;

        private int _numFlames;

        public MF(float xpos, float ypos, MAPF owner, int numFlames = 8)
            : base(xpos, ypos)
        {
            this._sprite = new SpriteMap("smallFire", 16, 16, false);
            this._sprite.AddAnimation("burn", 0.2f + Rando.Float(0.2f), true, new int[]
			{
				0,
				1,
				2,
				3,
				4
			});
            this._sprite.SetAnimation("burn");
            this._sprite.imageIndex = Rando.Int(4);
            this.graphic = this._sprite;
            this.center = new Vec2(8f, 8f);
            this.collisionOffset = new Vec2(-4f, -2f);
            this.collisionSize = new Vec2(8f, 4f);
            base.depth = -0.5f;
            this.thickness = 1f;
            this.weight = 1f;
            base.breakForce = 9999999f;
            this._owner = owner;
            this.weight = 0.5f;
            this.gravMultiplier = 0.7f;
            this._numFlames = numFlames;
        }

        protected override bool OnDestroy(DestroyType type = null)
        {
            if (base.isServerForObject)
            {
                for (int i = 0; i < this._numFlames; i++)
                {
                    Level.Add(SmallFire.New(base.x - this.hSpeed, base.y - this.vSpeed, -3f + Rando.Float(6f), -3f + Rando.Float(6f), false, null, true, this, false));
                }
            }
            SFX.Play("flameExplode", 0.9f, -0.1f + Rando.Float(0.2f), 0f, false);
            Level.Remove(this);
            return true;
        }

        public override void Update()
        {
            if (Rando.Float(2f) < 0.3f)
            {
                this.vSpeed += -2f + Rando.Float(3.5f);
            }
            if (Rando.Float(9f) < 0.1f)
            {
                this.vSpeed += -3f + Rando.Float(3.1f);
            }
            if (Rando.Float(14f) < 0.1f)
            {
                this.vSpeed += -5f + Rando.Float(4f);
            }
            if (Rando.Float(25f) < 0.1f)
            {
                this.vSpeed += -7f + Rando.Float(6f);
            }
            Level.Add(SmallSmoke.New(base.x, base.y));
            if (this.hSpeed > 0f)
            {
                this._sprite.angleDegrees = 90f;
            }
            else if (this.hSpeed < 0f)
            {
                this._sprite.angleDegrees = -90f;
            }
            base.Update();
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            if (base.isServerForObject && with != this._owner && !(with is Gun) && with.weight >= 5f)
            {
                if (with is PhysicsObject)
                {
                    with.hSpeed = this.hSpeed / 4f;
                    with.vSpeed -= 1f;
                }
                this.Destroy(new DTImpact(null));
                with.Burn(this.position, this);
            }
        }
    }
}
