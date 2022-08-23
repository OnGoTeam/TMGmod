#if FEATURES_1_2_X
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class Foucus : BaseAr, IHaveAllowedSkins
    {
        public int Legacy;

        public Foucus(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Foucus";
            ammo = 20;
            SetAmmoType<ATFOUCUS>();
            
            Smap = new SpriteMap(GetPath("Foucus"), 37, 13);
            _center = new Vec2(19f, 7f);
            _collisionOffset = new Vec2(-19f, -7f);
            _collisionSize = new Vec2(37f, 13f);
            _barrelOffsetTL = new Vec2(37f, 3f);
            _flare = new SpriteMap(GetPath("FlareFoucus"), 13, 10)
            {
                center = new Vec2(3f, 5f),
            };
            _holdOffset = new Vec2(3f, 2f);
            ShellOffset = new Vec2(-3f, -4f);
            _fireSound = GetPath("sounds/new/TC12-Silenced.wav");
            _fullAuto = true;
            _fireWait = 0.65f;
            _kickForce = 2f;
            KforceDelta = 1.5f;
            loseAccuracy = 0.275f;
            maxAccuracyLost = 0.275f;
            _weight = 8f;
        }

        [UsedImplicitly] public StateBinding LegacyBinding { get; } = new StateBinding(nameof(Legacy));
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });

        public override void Fire()
        {
            base.Fire();
            if (ammo < 1) return;
            if (duck == null) return;
            if (duck.ragdoll != null) return;
            if ((Legacy == 0) & (duck.vSpeed > -1f) & (duck.vSpeed < 1f)) duck.vSpeed += Rando.Float(-0.7f, -0.2f);
            else duck.vSpeed += Rando.Float(0.2f, 0.6f);
            Legacy = Rando.ChooseInt(0, 1);
        }
    }
}
#endif