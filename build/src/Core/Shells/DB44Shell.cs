using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class DB44Shell : EjectedShell
    {
        public DB44Shell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("44dbShell"))
        {
        }
    }
}