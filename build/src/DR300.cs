using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class DR300 : BaseDmr, IAmAr, IHaveSkin
    {
        private const int Postframe = 8;
        private const int NonSkinFrames = 3;

        private static readonly List<int> Allowedlst = new List<int>(new[] { 3, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public DR300(float xval, float yval)
            : base(xval, yval)
        {
            Rounds = new EditorProperty<int>(0, this, 0, 2, 1);
            skin = new EditorProperty<int>(8, this, -1f, 9f, 0.5f);
            PostRounds = Rando.ChooseInt(20, 30);
            ammo = PostRounds;
            _ammoType = new ATDR300();
            BaseAccuracy = 0.98f;
            MinAccuracy = 0.65f;
            RegenAccuracyDmr = 0.02f;
            DrainAccuracyDmr = 0.2f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("DR300"), 37, 11);
            _graphic = _sprite;
            _sprite.frame = Postframe;
            _center = new Vec2(18f, 6f);
            _collisionOffset = new Vec2(-18f, -6f);
            _collisionSize = new Vec2(37f, 11f);
            _barrelOffsetTL = new Vec2(37f, 2f);
            _fireSound = "deepMachineGun";
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fullAuto = false;
            _fireWait = 0.3f;
            _kickForce = 2.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(2f, 3f);
            ShellOffset = new Vec2(-7f, -2f);
            _editorName = "Daewoo DR300";
            laserSight = false;
            _weight = 3.5f;
        }

        [UsedImplicitly] public int PostRounds { get; private set; }

        [UsedImplicitly] public StateBinding PostRoundsBinding { get; } = new StateBinding(nameof(PostRounds));

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once ConvertToAutoProperty
        [UsedImplicitly] public EditorProperty<int> Rounds { get; }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            if ((PostRounds == 20) & !((_sprite.frame > 9) & (_sprite.frame < 20)))
                _sprite.frame = 10 + _sprite.frame % 10;
            if ((PostRounds == 30) & (_sprite.frame < 19)) _sprite.frame = 20 + _sprite.frame % 10;
            base.Update();
        }

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic)) bublic = Rando.Int(0, 9);
            _sprite.frame = bublic;
        }

        public override void EditorPropertyChanged(object property)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (Rounds.value)
            {
                case 0:
                    PostRounds = Rando.ChooseInt(20, 30);
                    break;
                case 1:
                    PostRounds = 20;
                    break;
                case 2:
                    PostRounds = 30;
                    break;
            }

            ammo = PostRounds;
            UpdateSkin();
            _sprite.frame = Rounds.value * 10 + _sprite.frame % 10;
            base.EditorPropertyChanged(property);
        }
    }
}
