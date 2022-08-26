using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.SkinLogic;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class Taligator6000Shell : EjectedShell, IHaveFrameId
    {
        private readonly SpriteMap _sprite;

        public Taligator6000Shell(float xpos, float ypos)
            : base(xpos, ypos, Mod.GetPath<TMGmod>("Taligator 6000 SXShell"))
        {
            _sprite = new SpriteMap(GetPath("Taligator 6000 SXShell"), 16, 16);
            _graphic = _sprite;
            scale *= 0.707f;
        }

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value.Modulo(10);
        }

        [UsedImplicitly] public StateBinding FrameIdBinding = new(nameof(FrameId));
    }
}
