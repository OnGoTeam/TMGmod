using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Other")]
    public class DragoShot : BaseBurst, IAmSr, IHaveAllowedSkins
    {
        private const int FramesToCharge = 50;
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public DragoShot(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 16;
            _ammoType = new ATDragoshot();
            SetAccuracyAsMax();
            _numBulletsPerFire = 8;

            _sprite = new SpriteMap(GetPath("Dragoshot"), 29, 11);
            _graphic = _sprite;
            SkinValue = -1;
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-14f, -7f);
            _collisionSize = new Vec2(29f, 11f);
            _barrelOffsetTL = new Vec2(29f, 2f);
            _fireSound = "shotgunFire";
            _fireWait = 1.5f;
            _kickForce = 5.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            laserSight = true;
            _laserOffsetTL = new Vec2(23f, 3f);
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-6f, -3f);
            _editorName = "DragoShot";
            _weight = 5f;
            DeltaWait = 0.15f;
            BurstNum = 1;
            Compose(
                new Charging(
                    this,
                    (counter, old) =>
                    {
                        var charged = counter >= FramesToCharge;
                        _ammoType.range = charged ? 170f : 120f;
                        MaxAccuracy = charged ? .9f : .7f;
                        maxAccuracyLost = charged ? .1f : .4f;
                        _kickForce = charged ? 3f : 5.5f;
                        BurstNum = charged ? 4 : 1;
                        if (charged && old < FramesToCharge) SFX.Play("woodHit");
                    },
                    _ => Fire()
                )
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void OnPressAction()
        {
        }
    }
}
