using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class AT40ACPShell : EjectedShell
    {
        public AT40ACPShell(float xpos, float ypos)
            : base(xpos, ypos, Mod.GetPath<TMGmod>("AT40ACPShell"))
        {
            scale *= 0.9f;
        }
    }
}
