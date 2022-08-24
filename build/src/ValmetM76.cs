using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Rifle|Burst")]
    public class ValmetM76 : BaseGun, IHaveAllowedSkins
    {
        public ValmetM76(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Valmet M76";
            ammo = 20;
            SetAmmoType<ATM76>();
            MinAccuracy = 0.5f;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("Valmet M76"), 33, 10);
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(33f, 3.5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(4f, 1f);
            ShellOffset = new Vec2(-5f, -1f);
            _fireSound = GetPath("sounds/new/DaewooK1.wav");
            _fullAuto = false;
            _fireWait = 0.7f;
            _kickForce = 3f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f; //0.4f
            _weight = 4f;
            var lose = new LoseAccuracy(0.15f, 0.02f, 1f);
            Compose(
                lose,
                new Burst(
                    this,
                    false,
                    2,
                    .15f,
                    burst => {
                        _fireWait = burst ? 1.4f : 0.7f;
                        NonSkin = burst ? 10 : 0;
                        loseAccuracy = burst ? 0f : 0.15f;
                        _kickForce = burst ? 6.5f : 3f;
                        MaxAccuracy = burst ? 1f : 0.89f;
                        lose.Regen = burst ? 0f : 0.02f;
                        lose.Drain = burst ? 0f : 0.15f;
                    }
                ).SwichingOnQuack()
            );
        }
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
