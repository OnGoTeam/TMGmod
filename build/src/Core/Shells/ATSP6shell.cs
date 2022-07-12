using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class ATSP6Shell : EjectedShell
    {
        public ATSP6Shell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("ATSP6Shell"))
        {
            scale *= 0.9f;
        }
    }
}