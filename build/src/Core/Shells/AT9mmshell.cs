using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class AT9mmShell : EjectedShell
    {
        public AT9mmShell(float xpos, float ypos)
            : base(xpos, ypos, Mod.GetPath<TMGmod>("AT9mmShell"))
        {
            scale *= 1f;
        }
    }
}
