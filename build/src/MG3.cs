using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG3 : BaseLmg, IHaveAllowedSkins, I5
    {
        [UsedImplicitly]
        public MG3(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "MG3";
            ammo = 80;
            NonSkinFrames = 6;
            Smap = new SpriteMap(GetPath("mg3"), 39, 11);
            _center = new Vec2(20f, 6f);
            _collisionOffset = new Vec2(-20f, -6f);
            _collisionSize = new Vec2(39f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _fireSound = GetPath("sounds/new/HighCaliber-Impactful.wav");
            _fullAuto = true;
            _fireWait = .5f;
            _kickForce = 2.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(4f, 2f);
            ShellOffset = new Vec2(-1f, -3f);
            _weight = 7f;
            SetAmmoType<AT556NATO>(.8f);
            Compose(
                new BifurcatedFw(this, 1.0f, .001f, .02f),
                new WithBipods(
                    this,
                    GetPath("sounds/beepods1"),
                    GetPath("sounds/beepods2"),
                    1f / 15f,
                    state =>
                    {
                        _ammoType.range = state.Deployed ? 550f : 480f;
                        _ammoType.bulletSpeed = state.Deployed ? 40f : 28f;
                        KickForce1Lmg = state.Deployed ? 0 : 2.0f;
                        KickForce2Lmg = state.Deployed ? 0 : 3.0f;
                        loseAccuracy = state.Deployed ? 0 : 0.1f;
                        maxAccuracyLost = state.Deployed ? 0 : 0.25f;
                        NonSkin = NonSkin % 2 + 2 * (state.Deployed ? 2 : state.Folded ? 0 : 1);
                    }
                ).Disableable()
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5 });

        protected override void OnInitialize()
        {
            _ammoType.range = 480f;
            base.OnInitialize();
        }

        public override void Update()
        {
            if (ammo == 0 && NonSkin % 2 == 0) NonSkin += 1;
            base.Update();
        }
    }
}
