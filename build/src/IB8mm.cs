using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class IB8mm : BaseGun, IHaveAllowedSkins
    {
        public IB8mm(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 26;
            _ammoType = new ATIB8();
            MaxAccuracy = _ammoType.accuracy;
            Smap = new SpriteMap(GetPath("IB-8mm Sniper"), 28, 12);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(14f, 6f);
            _collisionOffset = new Vec2(-14f, -6f);
            _collisionSize = new Vec2(28f, 12f);
            _barrelOffsetTL = new Vec2(28f, 5f);
            _fireSound = GetPath("sounds/Silenced2.wav");
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.15f;
            _holdOffset = new Vec2(-2f, 0f);
            ShellOffset = new Vec2(-3f, 0f);
            _editorName = "IB-8mm Sniper";
            _weight = 3f;
            Compose(new FirstKforce(11, kforce => kforce + 2f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
