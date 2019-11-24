using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Other")]
    public class DragoShot : BaseBurst, IAmSr, IHaveSkin
    {
        [UsedImplicitly]
        public float Counter;
        [UsedImplicitly]
        public StateBinding CounterBinding = new StateBinding(nameof(Counter));
        private const float Step = 0.01f;
        private const float TimeToHappend = 1f;
        [UsedImplicitly]
        public bool LoockerOfSound;
        [UsedImplicitly]
        public StateBinding LoockerOfSoundBinding = new StateBinding(nameof(LoockerOfSound));
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        public DragoShot (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(-1, this, -1f, 9f, 0.5f);
            ammo = 16;
            _ammoType = new ATMagnum
            {
                range = 120f,
                accuracy = 0.7f,
                penetration = 2f,
                bulletThickness = 0.8f
            };
            _numBulletsPerFire = 8;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Dragoshot"), 29, 11);
            _graphic = _sprite;
            _sprite.frame = Rando.Int(0, 9);
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-14f, -7f);
            _collisionSize = new Vec2(29f, 11f);
            _barrelOffsetTL = new Vec2(29f, 2f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 1.5f;
            _kickForce = 5.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            laserSight = true;
            _laserOffsetTL = new Vec2(23f, 3f);
            _holdOffset = new Vec2(0f, 3f);
            ShellOffset = new Vec2(-6f, -3f);
            _editorName = "DragoShot";
			_weight = 5f;
            DeltaWait = 0.15f;
            BurstNum = 1;
        }
        public override void OnPressAction()
        {
            //Nothing Happend
        }
        public override void OnHoldAction()
        {
            if (ammo != 0 && Counter <= TimeToHappend) Counter += Step;
            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
            Counter = 0f;
            base.OnReleaseAction();
            if (duck != null) Fire();
            LoockerOfSound = false;
        }
        public override void Update()
        {
            if (Counter >= TimeToHappend)
            {
                if (!LoockerOfSound) SFX.Play("woodHit");
                LoockerOfSound = true;
                _ammoType.range = 170f;
                _ammoType.accuracy = 0.9f;
                maxAccuracyLost = 0.1f;
                _kickForce = 3f;
                BurstNum = 4;
            }
            else
            {
                _ammoType.range = 120f;
                _ammoType.accuracy = 0.7f;
                maxAccuracyLost = 0.4f;
                _kickForce = 5.5f;
                BurstNum = 1;
            }
            base.Update();
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}