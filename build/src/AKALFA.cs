using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class AKALFA : BaseAr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Fid;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 4, 5 });
        public bool Stock = true;
        public StateBinding StockBinding = new StateBinding(nameof(Stock));

        public AKALFA (float xval, float yval)
          : base(xval, yval)
        {
            Fid = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new AT9mm
            {
                range = 425f,
                accuracy = 1f,
                penetration = 1.5f,
                bulletSpeed = 60f,
                bulletThickness = 0.87f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("ALFApattern"), 38, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _graphic = _sprite;
            _center = new Vec2(19f, 4.5f);
            _collisionOffset = new Vec2(-19f, -4.5f);
            _collisionSize = new Vec2(38f, 9f);
            _barrelOffsetTL = new Vec2(38f, 2.5f);
            _holdOffset = new Vec2(5f, 0f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 0.65f;
		    Kforce1Ar = 0.05f;
		    Kforce2Ar = 0.75f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.3f;
            _editorName = "Alfa";
			_weight = 5.5f;
            _laserOffsetTL = new Vec2(31f, 4f);
            laserSight = true;
		}
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Stock)
                {
                    _sprite.frame -= 10;
                    _ammoType.accuracy = 1f;
                    loseAccuracy = 0f;
                    Stock = false;
                    weight = 5.5f;
                }
                else
                {
                    _sprite.frame += 10;
                    _ammoType.accuracy = 0.92f;
                    loseAccuracy = 0.045f;
                    Stock = true;
                    weight = 3.5f;
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