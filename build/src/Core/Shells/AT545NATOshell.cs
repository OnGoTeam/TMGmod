using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class AT545NATOShell : EjectedShell
    {
        public AT545NATOShell(float xpos, float ypos)
            : base(xpos, ypos, Mod.GetPath<TMGmod>("AT545NATOShell"))
        {
            scale *= 1f;
        }
    }
}
