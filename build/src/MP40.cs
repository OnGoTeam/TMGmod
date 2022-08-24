using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [BaggedProperty("isInDemo", true)]
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class MP40 : BaseSmg, IHaveAllowedSkins, I5
    {
        [UsedImplicitly]
        public MP40(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "MP40";
            ammo = 32;
            SetAmmoType<ATMP40>(.99f);
            KforceDelta = 2f;
            KforceDelay = 20;
            Smap = new SpriteMap(GetPath("MP40"), 23, 14);
            _center = new Vec2(12f, 7f);
            _collisionOffset = new Vec2(-12f, -7f);
            _collisionSize = new Vec2(23f, 14f);
            _barrelOffsetTL = new Vec2(23f, 1f);
            _flare = FrameUtils.FlareOnePixel1();
            _fireSound = "deepMachineGun";
            _fireWait = 0.5f;
            _kickForce = 1.45f;
            _holdOffset = new Vec2(4f, 4f);
            ShellOffset = new Vec2(2f, -6f);
            _weight = 3f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            ComposeFirstAccuracy(20);
            Compose(new BifurcatedFw(this, 1.0f, .001f, .05f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5, 7, 8 });
    }
}
