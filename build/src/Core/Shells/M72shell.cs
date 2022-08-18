using DuckGame;

namespace TMGmod.Core.Shells
{
    public class M72Shell : EjectedShell
    {
        public M72Shell(float xpos, float ypos)
            : base(xpos, ypos, Mod.GetPath<TMGmod>("M72Shell"))
        {
            scale *= 2f / 3f;
        }
    }
}
