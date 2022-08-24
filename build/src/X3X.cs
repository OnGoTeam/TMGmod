using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Other")]
    [BaggedProperty("isSuperWeapon", true)]
    [UsedImplicitly]
    public sealed class X3X : BaseGun, IHaveAllowedSkins
    {
        private readonly Animating<object> _animating;

        public X3X(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Experimental X3X";
            _bio = "ammo = 1337";
            ammo = 6;
            SetAmmoType<ATx3x>();
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("X3X"), 27, 14);
            _center = new Vec2(14f, 9f);
            _collisionOffset = new Vec2(-11.5f, -9f);
            _collisionSize = new Vec2(27f, 14f);
            _barrelOffsetTL = new Vec2(27f, 5.5f);
            _fireSound = GetPath("sounds/new/X3X.wav");
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 8f;
            loseAccuracy = 1.7f;
            maxAccuracyLost = 1.7f;
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-5f, -2f);
            _weight = 5.5f;
            _animating = Anime.Simple(
                frames =>
                {
                    if (frames > 0 && owner is null)
                    {
                        _animating?.Cancel();
                        handOffset = new Vec2(0, 0f);
                        return;
                    }

                    handOffset = new Vec2(0, -3f) + new Vec2(-7f, 0f) * (
                        frames > 45 ? (50 - frames) / 5f : frames / 45f
                    );
                },
                () =>
                {
                    NonSkin = 0;
                    SFX.Play(GetPath("sounds/tuduc.wav"));
                    handOffset = new Vec2(0, 0f);
                }
            );
            Compose(_animating);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 5, 6, 7, 9 });

        public override void OnPressAction()
        {
            if (_animating.Active()) return;
            if (NonSkin == 1)
                _animating.Set(50);
            else
                base.OnPressAction();
        }

        protected override void BaseOnSpent()
        {
            NonSkin = ammo > 0 ? 1 : 2;
        }

        protected override void PopBaseShell()
        {
            ATx3x.PopShellSkin(Offset(ShellOffset).x, Offset(ShellOffset).y, offDir, FrameId, AddShell);
        }
    }
}
