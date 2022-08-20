using DuckGame;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|SMG|MP")]
    // ReSharper disable once InconsistentNaming
    public class ANP73 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public ANP73(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Experimental ANP-73";
            ammo = 33;
            SetAmmoType<ATANP73>();
            NonSkinFrames = 4;
            Smap = new SpriteMap(GetPath("Experimental ANP-73"), 19, 14);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(10f, 7f);
            _collisionOffset = new Vec2(-10f, -7f);
            _collisionSize = new Vec2(19f, 14f);
            _barrelOffsetTL = new Vec2(19f, 3f);
            _holdOffset = new Vec2(2f, 3f);
            ShellOffset = new Vec2(-1f, -3f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 1.5f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            _weight = 2f;
        }
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public override void Update()
        {
            if (Quacked())
            {
                if (NonSkin < 1) //1
                {
                    NonSkin += 1;
                    _fireWait = 1.2f;
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.5f;
                }
                else if (NonSkin < 2) //2
                {
                    NonSkin += 1;
                    _fireWait = 0.9f;
                    loseAccuracy = 0.2f;
                    maxAccuracyLost = 0.6f;
                }
                else if (NonSkin < 3) //3
                {
                    NonSkin += 1;
                    _fireWait = 0.6f;
                    loseAccuracy = 0.3f;
                    maxAccuracyLost = 0.7f;
                }
                else if (NonSkin < 4) //0
                {
                    NonSkin -= 3;
                    _fireWait = 1.5f;
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.3f;
                }

                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }
    }
}
