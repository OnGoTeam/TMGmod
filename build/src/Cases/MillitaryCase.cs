using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Custom_Guns;
#if DEBUG
using TMGmod.Useless_or_deleted_Guns;
#endif

namespace TMGmod.Cases.Color
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
            Things = new List<Type>
            {
                typeof(X3X)
            };
            CaseId = 0;
        }
    }
}