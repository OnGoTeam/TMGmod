using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|MP")]
    // ReSharper disable once InconsistentNaming
    public class SMG9 : BaseSmg, IHaveAllowedSkins
    {
        public SMG9(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "SMG-9";
            ammo = 24;
            SetAmmoType<ATSMG9>();
            KforceDelta = 3.5f;
            KforceDelay = 15;
            Smap = new SpriteMap(GetPath("SMG9"), 16, 15);
            _center = new Vec2(7f, 7f);
            _collisionOffset = new Vec2(-8f, -7f);
            _collisionSize = new Vec2(16f, 15f);
            _barrelOffsetTL = new Vec2(16f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/new/SMG-3.wav");
            _fullAuto = true;
            _fireWait = 0.35f;
            _kickForce = 1.6f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.57f;
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-1f, -3f);
            _weight = 2.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4 });
    }
}
