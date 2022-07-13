using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class Taligator6000Shell : EjectedShell
    {
        public Taligator6000Shell(float xpos, float ypos)
            : base(xpos, ypos, Mod.GetPath<TMGmod>("Taligator 6000 SXShell"))
        {
            scale *= 0.707f;
        }
    }
}
