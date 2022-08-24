using System.Collections.Generic;
using DuckGame;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Other")]
    public class DragoShot : BaseGun, IAmSr, IHaveAllowedSkins
    {
        private const int FramesToCharge = 50;
        private bool _charged;

        public DragoShot(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "DragoShot";
            ammo = 16;
            SetAmmoType<ATDragoshot>();
            _numBulletsPerFire = 8;

            Smap = new SpriteMap(GetPath("Dragoshot"), 29, 11);
            SkinValue = -1;
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-14f, -7f);
            _collisionSize = new Vec2(29f, 11f);
            _barrelOffsetTL = new Vec2(29f, 2.5f);
            _fireSound = GetPath("sounds/new/DragoShot.wav");
            _flare = new SpriteMap(GetPath("FlareBase3"), 13, 10)
            {
                center = new Vec2(0f, 4.5f),
            };
            _fireWait = 1.5f;
            _kickForce = 5.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            laserSight = true;
            _laserOffsetTL = new Vec2(23f, 4.5f);
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-6f, -3f);
            _weight = 5f;
            var burst = new Burst(
                this,
                false,
                4,
                .15f,
                enabled =>
                {
                    _ammoType.range = enabled ? 170f : 120f;
                    MaxAccuracy = enabled ? .9f : .7f;
                    maxAccuracyLost = enabled ? .1f : .4f;
                    _kickForce = enabled ? 3f : 5.5f;
                    _fireSound = enabled ? GetPath("sounds/new/DragoShot-Blast.wav") : GetPath("sounds/new/DragoShot.wav");
                }
            );
            Compose(
                burst,
                new Charging(
                    this,
                    (counter, old) =>
                    {
                        _charged = counter >= FramesToCharge;
                        burst.Switch(IsCharged);
                        if (_charged && old < FramesToCharge) SFX.Play("woodHit");
                    },
                    _ =>
                    {
                        if (!isServerForObject) return;
                        // else
                        if (owner is null) return;
                        // else
                        UpdateAction();
                        Fire();
                        _fireActivated = true;
                        UpdateAction();
                    }
                )
            );
        }

        private bool IsCharged(bool _) => _charged;

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        public override void OnPressAction()
        {
            if (receivingPress)
                base.OnPressAction();
        }
    }
}
