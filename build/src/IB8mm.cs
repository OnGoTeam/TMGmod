using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class IB8mm : BaseGun, IHaveAllowedSkins
    {
        public IB8mm(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "IB-8mm Sniper";
            ammo = 26;
            SetAmmoType<ATIB8>();
            Smap = new SpriteMap(GetPath("IB-8mm Sniper"), 28, 12);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(14f, 6f);
            _collisionOffset = new Vec2(-14f, -6f);
            _collisionSize = new Vec2(28f, 12f);
            _barrelOffsetTL = new Vec2(28f, 5.5f);
            _fireSound = GetPath("sounds/new/IB8mm.wav");
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.15f;
            _holdOffset = new Vec2(-2f, 0f);
            ShellOffset = new Vec2(-2f, 0f);
            _weight = 3f;
            Compose(new FirstKforce(11, kforce => kforce + 2f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
