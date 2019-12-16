using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    /// <inheritdoc />
    [EditorGroup("TMG|Misc")]
    [BaggedProperty("canSpawn", false)]
    [UsedImplicitly]
    public class C4Skeet : Holdable
    {
        /// <inheritdoc />
        public C4Skeet(float xpos, float ypos) : base(xpos, ypos)
        {
            _weight = 8f;
            _graphic = new Sprite(GetPath("c4skeet"));
            _center = new Vec2(4f, 2f);
            _collisionOffset = new Vec2(-4f, -2f);
            _collisionSize = new Vec2(8f, 4f);
        }

        /// <inheritdoc />
        public override void Update()
        {
            base.Update();
            if (Rando.Float(0f, 1f) > 0.01) return;
            //else
            var sCfour = new Cfour(x, y)
            {
                hSpeed = Rando.Float(-7f, 7f),
                vSpeed = Rando.Float(-5f, 5f) - 15f,
                Weak = true
            };
            Level.Add(sCfour);
        }
    }
}