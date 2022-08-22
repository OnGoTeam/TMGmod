using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.SkinLogic;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class X3XShell : EjectedShell, IHaveFrameId
    {
        private readonly SpriteMap _sprite;

        public X3XShell(float xpos, float ypos, int frameid)
            : base(xpos, ypos, Mod.GetPath<TMGmod>("X3XShell"))
        {
            _sprite = new SpriteMap(GetPath("X3XShell"), 16, 16);
            FrameId = frameid;
            _graphic = _sprite;
            scale *= 1f;
        }

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value.Modulo(10);
        }

        [UsedImplicitly] public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        public override void Update()
        {
            base.Update();
            _angle = Maths.DegToRad(-_spinAngle);
        }
    }
}
