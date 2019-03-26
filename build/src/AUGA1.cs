using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class AUGA1 : BaseAr, IHaveSkin
    {
        public bool Grip;
        public StateBinding GripBinding = new StateBinding(nameof(Grip));
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Fid;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6 });

        public AUGA1 (float xval, float yval)
          : base(xval, yval)
        {
            Fid = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 42;
            _ammoType = new ATMagnum
            {
                range = 500f,
                accuracy = 0.91f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("AUGA1pattern"), 30, 12);
            _graphic = _sprite;
            _sprite.frame = 3;
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 5f);
            _holdOffset = new Vec2(-3f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _editorName = "AUG A1";
			_weight = 5.5f;
            Kforce2Ar = 0.7f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Grip)
                {
                    _sprite.frame -= 10;
                    _fireWait = 0.8f;
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.2f;
                    _ammoType.accuracy = 0.91f;
                    Grip = false;
                }
                else
                {
                    _sprite.frame += 10;
                    _fireWait = 1.2f;
                    loseAccuracy = 0.25f;
                    maxAccuracyLost = 0.125f;
                    _ammoType.accuracy = 0.94f;
                    Grip = true;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
        }
        private void UpdateSkin()
        {
            var fid = Fid.value;
            while (!Allowedlst.Contains(fid))
            {
                fid = Rando.Int(0, 9);
            }
            _sprite.frame = fid;
        }
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