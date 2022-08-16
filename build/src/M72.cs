using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Explosive|Grenadelauncher")]
    [UsedImplicitly]
    public class M72 : BaseGun, IHaveAllowedSkins
    {
        public M72(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "M72";
            ammo = 5;
            SetAmmoType<ATM72>();
            NonSkinFrames = 6;
            Smap = new SpriteMap(GetPath("M72"), 32, 11);
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(32f, 11f);
            _barrelOffsetTL = new Vec2(32f, 2f);
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-1f, -(5f / 3f));
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 1f;
            _kickForce = 4.5f;
            loseAccuracy = 0.65f;
            maxAccuracyLost = 1f;
            _weight = 4.5f;
        }
        public override void Update()
        {
            if (ammo > 4) NonSkin = 0;
            else if (ammo > 3) NonSkin = 1;
            else if (ammo > 2) NonSkin = 2;
            else if (ammo > 1) NonSkin = 3;
            else if (ammo > 0) NonSkin = 4;
            else NonSkin = 5;
            base.Update();
        }
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
