using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class PP19 : BaseGun, IHaveAllowedSkins
    {
        public PP19(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "PP-19 Bizon";
            ammo = 64;
            SetAmmoType<ATBizon>(.8f);
            ComposeFirstAccuracy(25);
            MinAccuracy = 0.2f;
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("PP19Bizon"), 28, 9);
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(28f, 9f);
            _barrelOffsetTL = new Vec2(28f, 2.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _fireSound = GetPath("sounds/new/SMG-2.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.35f;
            _holdOffset = new Vec2(2f, 1f);
            handOffset = new Vec2(2f, 0f);
            ShellOffset = new Vec2(-1f, -2f);
            _weight = 1.5f;
            Compose(
                new WithStock(
                    this,
                    true,
                    GetPath("sounds/tuduc"),
                    GetPath("sounds/tuduc"),
                    1f / 10f,
                    state =>
                    {
                        _fireWait = state.Deployed ? 0.75f : 0.5f;
                        loseAccuracy = state.Deployed ? 0.15f : 0.25f;
                        maxAccuracyLost = state.Deployed ? 0.35f : 0.7f;
                        _weight = state.Deployed ? 1.5f : 1f;
                        NonSkin = state.Deployed ? 0 : state.Folded ? 2 : 1;
                    }
                ).Switching()
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 8 });
    }
}
