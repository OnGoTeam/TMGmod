using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class RemingtonTac : BasePumpAction, IHaveAllowedSkins
    {
        public RemingtonTac(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Remington 870 Raid";
            ammo = 4;
            SetAmmoType<ATRemington>();
            _numBulletsPerFire = 6;
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("Remington 870 Raid"), 26, 8);
            _center = new Vec2(13f, 4f);
            _collisionOffset = new Vec2(-13f, -4f);
            _collisionSize = new Vec2(26f, 8f);
            _barrelOffsetTL = new Vec2(26f, 2.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _holdOffset = new Vec2(0f, 1f);
            _fireSound = "shotgunFire2";
            _kickForce = 2.8f;
            _manualLoad = true;
            _fireWait = 1.5f;
            _laserOffsetTL = new Vec2(22f, 1.5f);
            laserSight = true;
            LoaderSprite = new SpriteMap(GetPath("Remington 870 RaidPump"), 5, 2)
            {
                center = new Vec2(3f, 1f),
            };
            ShellOffset = new Vec2(2f, -2f);
            LoaderVec2 = new Vec2(8f, 0f);
            Loaddx = 3f;
            _stock = new WithStock(
                this,
                true,
                GetPath("sounds/beepods1"),
                GetPath("sounds/beepods2"),
                1f / 10f,
                state =>
                {
                    _kickForce = state.Deployed ? 1.1f : 2.8f;
                    _fireWait = state.Deployed ? 0f : 2.75f;
                    LoadSpeed = (sbyte)(state.Deployed ? 20 : 10);
                    NonSkin = state.Deployed ? 0 : state.Folded ? 2 : 1;
                }
            );
            Compose(
                _stock.Switching()
            );
        }

        private readonly WithStock _stock;

        private static float Rmax => 3.506401f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 8, 9 });


        protected override void UpdateFrameId(int frameId)
        {
            SetSpriteMapFrameId(LoaderSprite, frameId, SkinFrames);
        }

        private void GottaGoFast()
        {
            var runMax = duck.runMax;
            duck.runMax = Rmax;
            duck.UpdateMove();
            duck.runMax = runMax;
        }

        public override void Update()
        {
            base.Update();
            if (_stock.Folded() && duck != null && ammo > 0 && (ammo > 1 || loaded)) GottaGoFast();
        }
    }
}
