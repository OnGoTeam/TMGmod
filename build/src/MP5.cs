using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MP5 : BaseBurst, IFirstKforce, IHaveSkin, IAmSmg
    {

        private readonly SpriteMap _sprite;
        public bool NonAuto = true;
        public StateBinding NonAutoBinding = new StateBinding(nameof(NonAuto));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 3, 4, 6, 7 });
        public MP5(float xval, float yval)
            : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 215f,
                accuracy = 0.7f
            };
            BaseAccuracy = 0.7f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MP5pattern"), 27, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(13.5f, 6f);
            _collisionOffset = new Vec2(-13.5f, -6f);
            _collisionSize = new Vec2(27f, 12f);
            _barrelOffsetTL = new Vec2(27f, 3f);
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 0.3f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(-1f, 2f);
            _editorName = "MP5";
			_weight = 3f;
            KforceDSmg = 2f;
            MaxDelaySmg = 50;
            DeltaWait = 0.65f;
            BurstNum = 1;
        }
        private void UpdateSkin()
        {
            var fid = Skin.value;
            while (!Allowedlst.Contains(fid))
            {
                fid = Rando.Int(0, 9);
            }
            _sprite.frame = fid;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (NonAuto)
                {
                    NonAuto = false;
                    BurstNum = 3;
                    _fireWait = 1.3f;
                    _sprite.frame += 10;
                }
                else
                {
                    NonAuto = true;
                    BurstNum = 1;
                    _fireWait = 0.3f;
                    _sprite.frame -= 10;
                }
            }
            base.Update();
        }
        public float KforceDSmg { get; }
        public int CurrDelaySmg { get; set; }
        public int MaxDelaySmg { get; set; }
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
