using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Combined")]
    // ReSharper disable once InconsistentNaming
    public class MP5SD : MP5
    {
        public override ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 4, 6, 7 });

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public MP5SD(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATMP5SD();
            IncreasedAccuracy = 0.92f;
            LowerAccuracyFp = 0.77f;
            MaxAccuracy = LowerAccuracyFp;
            _type = "gun";
            Texture = new SpriteMap(GetPath("MP5SD"), 31, 12);
            _graphic = Texture;
            Texture.frame = 0;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(15.5f, 6f);
            _collisionOffset = new Vec2(-15.5f, -6f);
            _collisionSize = new Vec2(31f, 12f);
            _barrelOffsetTL = new Vec2(31f, 2f);
            _fireSound = GetPath("sounds/Silenced2.wav");
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "MP5SD";
        }
    }
}
