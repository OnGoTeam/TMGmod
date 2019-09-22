using DuckGame;

namespace TMGmod.Core.Shells
{
    public class M72Shell : EjectedShell
    {
        public M72Shell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<Core.TMGmod>("M72Shell"), "metalBounce")
        {
        }
    }
}