using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.Modifiers.Updating;
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
            var silencerProperty = new SynchronizedProperty<bool>(
                () => _fireSound == GetPath("sounds/new/CZ-Silenced.wav"),
                (old, value) =>
                {
                    if (value != old)
                        FrameUtils.SwitchedSilencer(old);
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
            Compose(
                silencerProperty,
                new Quacking(this, true, true, silencerProperty.Flip, "silencer", () => barrelOffset)
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 4, 5, 7 });

        public override void Update()
        {
            base.Update();
            if (ammo > 26) NonSkin = 5 * (NonSkin / 5) + 0;
            else if (ammo > 20) NonSkin = 5 * (NonSkin / 5) + 1;
            else if (ammo > 12) NonSkin = 5 * (NonSkin / 5) + 2;
            else if (ammo > 5) NonSkin = 5 * (NonSkin / 5) + 3;
            else NonSkin = 5 * (NonSkin / 5) + 4;
        }
    }
}
