using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class CZ805 : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public CZ805(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "CZ-805 BREN";
            ammo = 30;
            SetAmmoType<ATCZ>();
            NonSkinFrames = 10;
            Smap = new SpriteMap(GetPath("CZ805Bren"), 41, 11);
            _center = new Vec2(21f, 6f);
            _collisionOffset = new Vec2(-21f, -6f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _holdOffset = new Vec2(5f, 2f);
            ShellOffset = new Vec2(-5f, -3f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f;
            _weight = 5f;
            _kickForce = 1.5f;
            KforceDelta = 1.26f;
            ComposeSilencer(
                () => _fireSound == GetPath("sounds/new/CZ-Silenced.wav"),
                value =>
                {
                    NonSkin = NonSkin % 5 + 5 * (value ? 1 : 0);
                    _fireSound = value ? GetPath("sounds/new/CZ-Silenced.wav") : "deepMachineGun2";
                    _flare = value ? FrameUtils.TakeZis() : FrameUtils.FlareOnePixel1();
                    if (value)
                        SetAmmoType<ATCZS>();
                    else
                        SetAmmoType<ATCZ>();
                    _barrelOffsetTL = value ? new Vec2(41f, 3.5f) : new Vec2(39f, 3.5f);
                    maxAccuracyLost = value ? .35f : .25f;
                }
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 4, 5, 7 });

        public override void Update()
        {
            base.Update();
            NonSkin = ammo switch
            {
                > 26 => 5 * (NonSkin / 5) + 0,
                > 20 => 5 * (NonSkin / 5) + 1,
                > 12 => 5 * (NonSkin / 5) + 2,
                > 5 => 5 * (NonSkin / 5) + 3,
                _ => 5 * (NonSkin / 5) + 4,
            };
        }
    }
}
