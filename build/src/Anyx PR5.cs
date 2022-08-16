using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Burst")]
    public class Glock18 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public Glock18(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Anyx PR5";
            Smap = new SpriteMap(GetPath("Anyx PR5"), 12, 8);
            _center = new Vec2(6f, 4f);
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(12f, 8f);
            ammo = 21;
            SetAmmoType<ATPR5>();
            
            _barrelOffsetTL = new Vec2(12f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/new/HighCaliber-Pistol.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.6f;
            ShellOffset = new Vec2(-2f, -2f);
            _holdOffset = new Vec2(-1f, 2f);
            _weight = 1.7f;
            ComposeSimpleBurst(3, .6f);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 4 });
    }
}
