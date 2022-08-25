#if FEATURES_1_2_X
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|NOTRELEASEDYET")]
    [UsedImplicitly]
    public class Lstk16V6 : BaseGun, IHaveAllowedSkins
    {
        public Lstk16V6(float xval, float yval) : base(xval, yval)
        {
            _editorName = "LSTK-16v6";
            ammo = 25;
            SetAmmoType<ATM16>();
            NonSkinFrames = 9;
            Smap = new SpriteMap(GetPath("LSTK16v6"), 33, 14);
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-17f, -7f);
            _collisionSize = new Vec2(33f, 14f);
            _barrelOffsetTL = new Vec2(31f, 5f);
            _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
            {
                center = new Vec2(0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 5.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.45f;
            _holdOffset = new Vec2(3f, 0f);
            ShellOffset = new Vec2(-5f, -2f);
            _weight = 6.7f;
            Compose(
                new WithBipods(
                    this,
                    GetPath("sounds/beepods1"),
                    GetPath("sounds/beepods2"),
                    1f / 10f,
                    state =>
                    {
                        _kickForce = state.Deployed ? 0f : 5.5f;
                        NonSkin = NonSkin % 3 + 3 * (state.Deployed ? 2 : state.Folded ? 0 : 1);
                    }
                ).Disableable()
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
#endif
