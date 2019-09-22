using DuckGame;

namespace TMGmod.Core.Shells
{
    public class M50Shell : EjectedShell
    {
        public M50Shell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("M50Shell"))
        {
        }
    }
}