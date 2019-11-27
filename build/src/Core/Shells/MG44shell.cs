using DuckGame;

namespace TMGmod.Core.Shells
{
    public class MG44Shell : EjectedShell
    {
        public MG44Shell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("MG44Shell"))
        {
        }
    }
}