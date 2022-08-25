using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG44 : BaseLmg, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public MG44(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "MG44 Mark2H";
            ammo = 60;
            SetAmmoType<ATMG44>();
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("MG44 Mark2H"), 39, 11);
            _center = new Vec2(20f, 6f);
            _collisionOffset = new Vec2(-20f, -6f);
            _collisionSize = new Vec2(39f, 11f);
            _barrelOffsetTL = new Vec2(39f, 2.5f);
            _flare = FrameUtils.FlareOnePixel5();
            _fireSound = GetPath("sounds/new/LMG-3.wav");
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 1.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(5f, 1f);
            ShellOffset = new Vec2(-1f, -4f);
            _weight = 7.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 6, 7 });

        public override void Update()
        {
            KickForce1Lmg = HandleQ() ? .9f : 1.8f;
            KickForce2Lmg = HandleQ() ? 1.5f : 1.8f;
            loseAccuracy = HandleQ() ? 0f : 0.1f;
            maxAccuracyLost = HandleQ() ? 0f : 0.3f;
            base.Update();
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (ammo)
            {
                case 1:
                    NonSkin = 1;
                    break;
                case 0:
                    NonSkin = 2;
                    break;
            }
        }

        protected override void PopBaseShell()
        {
            ATMG44.PopShellSkin(Offset(ShellOffset).x, Offset(ShellOffset).y, offDir, FrameId, AddShell);
        }
    }
}
