using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class MG44Shell : EjectedShell, IHaveFrameId
    {
        private readonly SpriteMap _sprite;
        public MG44Shell(float xpos, float ypos, int frameid)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("MG44Shell"))
        {
            _sprite = new SpriteMap(GetPath("MG44Shell"), 16, 16);
            FrameId = frameid;
            _graphic = _sprite;
        }
        public override void Update()
        {
            base.Update();
            _angle = Maths.DegToRad(-_spinAngle);
        }
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % 10;
        }
        [UsedImplicitly]
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
    }
}