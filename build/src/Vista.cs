using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Burst")]
    // ReSharper disable once InconsistentNaming
    public class Vista : BaseGun, IHaveAllowedSkins, I5
    {
        [UsedImplicitly]
        public Vista(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Vista";

            Smap = new SpriteMap(GetPath("Vista"), 16, 14);
            _center = new Vec2(6f, 7f);
            _collisionOffset = new Vec2(-8f, -7f);
            _collisionSize = new Vec2(16f, 14f);
            _holdOffset = new Vec2(-1f, 3f);
            _barrelOffsetTL = new Vec2(16f, 2f);
            _flare = FrameUtils.FlareOnePixel0();
            ShellOffset = new Vec2(-4f, -3f);

            ammo = 30;
            SetAmmoType<ATVista>(1f);
            ComposeFirstAccuracy(30);
            ComposeSimpleBurst(3, .1f);
            _fireWait = 0.36f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.44f;
            _kickForce = 1.5f;
            _weight = 2f;
            ShellOffset = new Vec2(2f, -4f);

            _fireSound = GetPath("sounds/2.wav");
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 5 });
    }
}
