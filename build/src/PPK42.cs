using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class PPK42 : BaseSmg, IHaveAllowedSkins, I5
    {
        [UsedImplicitly]
        public PPK42(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "PPK 42";
            ammo = 30;
            SetAmmoType<ATPPK42>();
            KforceDelta = 2.5f;
            KforceDelay = 20;
            Smap = new SpriteMap(GetPath("PPK42"), 25, 11);
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(25f, 11f);
            _barrelOffsetTL = new Vec2(25f, 1f);
            _flare = new SpriteMap(GetPath("FlarePPK42"), 13, 10)
            {
                center = new Vec2(0.0f, 5.5f),
            };
            _fireSound = GetPath("sounds/new/SMG-2.wav");
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 1.5f;
            _holdOffset = new Vec2(4f, 2f);
            ShellOffset = new Vec2(-3f, -3f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _weight = 3f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7 });
    }
}
