using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class CZ75 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        private int _fdelay;

        public CZ75(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "CZ-75";
            ammo = 24;
            SetAmmoType<ATCZ75>();
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("CZ75"), 12, 8);
            _center = new Vec2(6f, 4f);
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(12f, 8f);
            _barrelOffsetTL = new Vec2(12f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-1f, 2f);
            ShellOffset = new Vec2(-3f, -1f);
            _fireSound = GetPath("sounds/1.wav");
            _fireWait = 0.75f;
            _kickForce = 0.9f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            _weight = 1f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 5 });

        public override void OnPressAction()
        {
            if (((ammo > 0 && NonSkin == 1) || (ammo > 12 && NonSkin == 0)) && _fdelay == 0)
                Fire();
            else
                switch (ammo)
                {
                    case 0:
                        DoAmmoClick();
                        break;
                    case 12 when NonSkin == 0:
                        SFX.Play("click");
                        if (_raised)
                            Level.Add(new Czmag(x, y + 1));
                        else if (offDir < 0)
                            Level.Add(new Czmag(x + 5, y));
                        else
                            Level.Add(new Czmag(x - 5, y));
                        NonSkin = 1;
                        _fdelay = 40;
                        break;
                    default:
                        DoAmmoClick();
                        break;
                }
        }

        public override void Update()
        {
            base.Update();
            if (_fdelay > 1)
            {
                _fdelay -= 1;
            }
            else if (_fdelay == 1)
            {
                SFX.Play("click");
                _fdelay -= 1;
            }
        }


        protected override float BaseKforce => NonSkin == 0 ? _kickForce : _kickForce * 216f;
    }
}
