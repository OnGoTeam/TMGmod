using System.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.NY;

namespace TMGmod.Cases
{
    [EditorGroup("TMG|Misc|Cases")]
    [UsedImplicitly]
    public class PodarokMillitary : BaseCase
    {
        public PodarokMillitary(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseMillitary"), 14, 8);
            _graphic = sprite;
            sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Millitary Case";
            CaseColor = 0;
        }

        public override void Initialize()
        {
            var physicsObjects = ItemBox.GetPhysicsObjects(Editor.Placeables);
            physicsObjects.RemoveAll(
                t => !(
                    t.IsSubclassOf(typeof(Gun)) &&
                    t.Assembly == typeof(Core.TMGmod).Assembly &&
                    t.Namespace != typeof(CandyCane).Namespace
                )
            );
            ThingsDetailed = physicsObjects.Select(B).ToList();
            base.Initialize();
        }

        protected override void Spawned(Holdable thing)
        {
            switch (thing)
            {
                case I5 i5:
                    i5.FrameId = 5;
                    break;
            }
        }
    }
}
