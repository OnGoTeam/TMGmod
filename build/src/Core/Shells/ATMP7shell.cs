using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class ATMP7Shell : EjectedShell
    {
        public ATMP7Shell(float xpos, float ypos)
            : base(xpos, ypos, Mod.GetPath<TMGmod>("ATMP7Shell"))
        {
            scale *= 0.625f;
        }
    }
}
