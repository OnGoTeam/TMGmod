#if DEBUG
using DuckGame;
using JetBrains.Annotations;


namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [BaggedProperty("canSpawn", false)]
    [PublicAPI]
    public class C4Pack:Holdable
    {
        public C4Pack(float xpos, float ypos) : base(xpos, ypos)
        {
            _weight = 3f;
            _graphic = new Sprite(GetPath("cfour"));
            _center = new Vec2(3f, 1.5f);
            _collisionOffset = new Vec2(-1.5f, -1.5f);
            _collisionSize = new Vec2(3f, 3f);
            flammable = 0.9f;
            thickness = 1f;
            throwSpeedMultiplier = 1.5f;
            airFrictionMult = 0.05f;
        }

        public override void OnPressAction()
        {
            for (var i = 0; i < 10; i++)
            {
                var c4 = new Cfour(x, y);
                Level.Add(c4);
            }

            Destroy();
            Level.Remove(this);
        }
    }
}
#endif