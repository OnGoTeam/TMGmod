using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Explosive|Grenadelauncher")]
    [UsedImplicitly]
    public class M72 : BaseGun, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 6;
        private readonly SpriteMap _sprite;

        public M72(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 5;
            _ammoType = new ATM72();
            MaxAccuracy = 0.95f;
            
            _sprite = new SpriteMap(GetPath("M72"), 32, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(32f, 11f);
            _barrelOffsetTL = new Vec2(32f, 2f);
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 1f;
            _kickForce = 4.5f;
            loseAccuracy = 0.65f;
            maxAccuracyLost = 1f;
            _editorName = "M72";
            _weight = 4.5f;
        }
        public override void Update()
        {
            if (ammo > 3 && ammo <= 4 && FrameId / 10 != 1) _sprite.frame += 10;
            if (ammo > 2 && ammo <= 3 && FrameId / 10 != 2) _sprite.frame += 10;
            if (ammo > 1 && ammo <= 2 && FrameId / 10 != 3) _sprite.frame += 10;
            if (ammo > 0 && ammo <= 1 && FrameId / 10 != 4) _sprite.frame += 10;
            if (ammo < 1 && FrameId / 10 != 5) _sprite.frame += 10;
            base.Update();
        }
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
