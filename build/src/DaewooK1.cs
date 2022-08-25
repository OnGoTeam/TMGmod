using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|PDW")]
    public class DaewooK1 : BaseSmg, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public DaewooK1(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Daewoo K1";
            ammo = 32;
            SetAmmoType<ATDaewooK1>();
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("DaewooK1"), 28, 11);
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(28f, 11f);
            _barrelOffsetTL = new Vec2(28f, 2.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _holdOffset = new Vec2(-2f, 2f);
            ShellOffset = new Vec2(1f, -2f);
            _fireSound = GetPath("sounds/new/DaewooK1.wav");
            _fullAuto = true;
            _fireWait = 0.86f;
            _kickForce = 0.5f;
            KforceDelta = 2.5f;
            KforceDelay = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.24f;
            _weight = 4.5f;
            Compose(
                new WithStock(
                    this,
                    true,
                    GetPath("sounds/beepods1"),
                    GetPath("sounds/beepods2"),
                    1f / 10f,
                    state =>
                    {
                        _fireWait = state.Deployed ? 0.86f : 0.75f;
                        loseAccuracy = state.Deployed ? 0.1f : 0.2f;
                        maxAccuracyLost = state.Deployed ? 0.24f : 0.4f;
                        _weight = state.Deployed ? 4.5f : 3f;
                        NonSkin = state.Deployed ? 0 : state.Folded ? 2 : 1;
                    }
                ).Switching()
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 7 });
    }
}
