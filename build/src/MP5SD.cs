using System.Collections.Generic;
using DuckGame;
using TMGmod.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Combined")]
    // ReSharper disable once InconsistentNaming
    public class MP5SD : MP5
    {
        public MP5SD(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "MP5SD";
            ammo = 30;
            IncreasedAccuracy = .92f;
            DecreasedAccuracy = .77f;
            SetAmmoType<ATMP5SD>(DecreasedAccuracy);
            Smap = new SpriteMap(GetPath("MP5SD"), 31, 12);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(31f, 12f);
            _barrelOffsetTL = new Vec2(31f, 2f);
            _fireSound = GetPath("sounds/new/MP5SD.wav");
            _holdOffset = new Vec2(1f, 2f);
            ShellOffset = new Vec2(0f, -4f);
        }

        public override ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 4, 6, 7 });
    }
}
