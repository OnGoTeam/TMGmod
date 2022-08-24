using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Burst")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class MP5 : BaseGun, IHaveAllowedSkins, IAmSmg
    {
        protected float IncreasedAccuracy;

        public MP5(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "MP5A4";
            ammo = 30;
            IncreasedAccuracy = .9f;
            DecreasedAccuracy = .7f;
            SetAmmoType<ATMP5>(DecreasedAccuracy);
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("MP5"), 27, 12);
            _center = new Vec2(13f, 6f);
            _collisionOffset = new Vec2(-13f, -6f);
            _collisionSize = new Vec2(27f, 12f);
            _barrelOffsetTL = new Vec2(27f, 2f);
            _flare = FrameUtils.FlareOnePixel1();
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(-2f, 2f);
            ShellOffset = new Vec2(2f, -4f);
            _weight = 3f;
            Compose(
                new FirstKforce(20, kforce => kforce + 1.2f),
                new FirstAccuracy(10, accuracy => DecreasedAccuracy),
                new Burst(
                    this,
                    false,
                    3,
                    .45f,
                    burst =>
                    {
                        _fireWait = burst ? 1.8f : 0.5f;
                        NonSkin = burst ? 1 : 0;
                        MaxAccuracy = burst ? IncreasedAccuracy : DecreasedAccuracy;
                    }
                ).SwichingOnQuack()
            );
        }

        protected float DecreasedAccuracy { get; set; }
        public virtual ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 4, 6, 7 });
    }
}
