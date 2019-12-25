using DuckGame;

namespace TMGmod.Core.Shells
{
    public class _44DBShell : EjectedShell
    {
        public _44DBShell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("44dbShell"))
        {
        }
    }
}