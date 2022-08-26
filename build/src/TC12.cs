using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class TC12 : BaseDmr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public TC12(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "TC-12";
            ammo = 11;
            SetAmmoType<ATTC12>();
            MinAccuracy = 0.45f;
            RegenAccuracyDmr = 0.007f;
            DrainAccuracyDmr = 0.15f;
            MaxDrain = .55f;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("TC-12"), 39, 12);
            _center = new Vec2(20f, 5f);
            _collisionOffset = new Vec2(-20f, -5f);
            _collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(28f, 3f);
            _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(6f, 0f);
            ShellOffset = new Vec2(-7f, -1f);
            _fireSound = GetPath("sounds/new/HighCaliber.wav");
            _fullAuto = false;
            _fireWait = 1.03f;
            _kickForce = 5.3f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            laserSight = true;
            _laserOffsetTL = new Vec2(26f, 5.5f);
            _weight = 4.5f;
            ComposeSilencer(
                () => _fireSound == GetPath("sounds/new/TC12-Silenced.wav"),
                value =>
                {
                    NonSkin = value ? 1 : 0;
                    _fireSound =
                        value ? GetPath("sounds/new/TC12-Silenced.wav") : GetPath("sounds/new/HighCaliber.wav");
                    if (value)
                        SetAmmoType<ATTC12S>();
                    else
                        SetAmmoType<ATTC12>();
                    _kickForce = value ? 4.5f : 5.3f;
                    loseAccuracy = value ? 0f : .1f;
                    _weight = value ? 6.3f : 4.5f;
                    _barrelOffsetTL = value ? new Vec2(39f, 3f) : new Vec2(28f, 3f);
                    _flare = value
                        ? new SpriteMap(GetPath("FlareSilencer"), 13, 10) { center = new Vec2(0.0f, 5f) }
                        : new SpriteMap(GetPath("FlareTC12"), 13, 10) { center = new Vec2(0.0f, 5f) };
                }
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3 });
    }
}
