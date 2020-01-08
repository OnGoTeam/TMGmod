using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class Gauge12Shell : EjectedShell
    {
        public Gauge12Shell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("12gaugeShell"))
        {
        }
    }
}