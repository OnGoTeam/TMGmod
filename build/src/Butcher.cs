using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class Butcher : BaseLmg, IHaveAllowedSkins
    {
        private float _debris = 1f;

        public Butcher(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Solaris Butcher";
            ammo = 180;
            SetAmmoType<ATButcher>();
            NonSkinFrames = 12;
            Smap = new SpriteMap(GetPath("Solaris Butcher"), 24, 12);
            _center = new Vec2(12f, 6f);
            _collisionOffset = new Vec2(-12f, -6f);
            _collisionSize = new Vec2(24f, 12f);
            _barrelOffsetTL = new Vec2(24f, 4f);
            _flare = new SpriteMap(GetPath("FlareMG44"), 13, 10)
            {
                center = new Vec2(1.0f, 6f),
            };
            _fireSound = GetPath("sounds/new/LMG-2.wav");
            _fullAuto = true;
            _fireWait = 0.2f;
            _kickForce = 0f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(3f, -1f);
            _weight = 4f;
            KickForce1Lmg = 0.33f;
            KickForce2Lmg = 0.67f;
            var magOffset = new Vec2(1f, 3f);
            var magInserted = new SynchronizedValue<bool>(true);
            Compose(
                magInserted,
                new Reloading(
                    this,
                    60,
                    magOffset,
                    (
                        load, magsBefore
                    ) =>
                    {
                        loaded = false;
                        if (magInserted.Value)
                        {
                            SFX.Play(GetPath("sounds/tuduc.wav"));
                            var magpos = Offset(magOffset);
                            Level.Add(
                                new ArwaMag(magpos.x, magpos.y) { graphic = { flipH = offDir < 0 } }
                            );
                            // NonSkin = magsBefore > 0 ? 1 : 2;
                            _wait += 5f;
                            magInserted.Value = false;
                            return;
                        }

                        load(
                            magsAfter =>
                            {
                                SFX.Play(GetPath("sounds/tuduc.wav"));
                                // NonSkin = magsAfter > 0 ? 0 : 3;
                                _wait += _fireWait;
                                magInserted.Value = true;
                            },
                            () => loaded = true
                        );
                    }
                )
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public override void Update()
        {
            if (ammoType.barrelAngleDegrees > 5f) _debris = -1f;
            if (ammoType.barrelAngleDegrees < -5f) _debris = 1f;
            base.Update();
        }

        public override void Fire()
        {
            base.Fire();
            ammoType.barrelAngleDegrees += _debris;
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            ammoType.barrelAngleDegrees = -5f;
            loaded = true;
        }
    }
}
