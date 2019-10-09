using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

namespace TMGmod.Cases
{
    /// <inheritdoc />
    [EditorGroup("TMG|Misc|Cases")]
    [UsedImplicitly]
    public class PodarokMillitary : BaseCase
    {
        /// <inheritdoc />
        public PodarokMillitary(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("MillitaryCase"), 14, 8);
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
            CaseId = 0;
        }

        public override void Initialize()
        {
            var physicsObjects = ItemBox.GetPhysicsObjects(Editor.Placeables);
            physicsObjects.RemoveAll(t => !(t.IsSubclassOf(typeof(Gun)) &&
                                            t.Assembly == typeof(Core.TMGmod).Assembly &&
                                            t.Namespace != typeof(NY.CandyCane).Namespace));
            Things = new List<Type>
            {
                typeof(X3X) //здесь должно быть всё оружие
            };
            Things = physicsObjects;
            base.Initialize();
        }

        protected override void Spawned(Holdable thing)
        {
            if (thing is I5 && thing is IHaveSkin skinThing)
            {
                skinThing.FrameId = 5;
            }
        }
    }
}