using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class DR300 : BaseDmr, IAmAr, IHaveAllowedSkins
    {
        public DR300(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Daewoo DR300";
            Rounds = new EditorProperty<int>(0, this, 0, 2, 1);
            PostRounds = Rando.ChooseInt(20, 30);
            ammo = PostRounds;
            SetAmmoType<ATDR300>();
            MinAccuracy = 0.65f;
            RegenAccuracyDmr = 0.02f;
            DrainAccuracyDmr = 0.2f;
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("DR300"), 37, 11);
            SkinValue = 8;
            _center = new Vec2(18f, 6f);
            _collisionOffset = new Vec2(-18f, -6f);
            _collisionSize = new Vec2(37f, 11f);
            _barrelOffsetTL = new Vec2(37f, 2f);
            _fireSound = "deepMachineGun";
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fullAuto = false;
            _fireWait = 0.3f;
            _kickForce = 2.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(2f, 3f);
            ShellOffset = new Vec2(-3f, -3f);
            laserSight = false;
            _weight = 3.5f;
        }

        [UsedImplicitly] public int PostRounds { get; private set; }

        [UsedImplicitly] public StateBinding PostRoundsBinding { get; } = new StateBinding(nameof(PostRounds));

        // ReSharper disable once InconsistentNaming

        [UsedImplicitly] public EditorProperty<int> Rounds { get; }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 3, 8 });

        public override void Update()
        {
            switch (PostRounds)
            {
                case 20:
                    NonSkin = 1;
                    break;
                case 30:
                    NonSkin = 2;
                    break;
            }

            base.Update();
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
            NonSkin = Rounds.value;
            base.EditorPropertyChanged(property);
        }
    }
}
