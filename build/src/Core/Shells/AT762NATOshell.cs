using DuckGame;

namespace TMGmod.Core.Shells
{
    public class AT762NATOShell : EjectedShell
    {
        public AT762NATOShell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("AT762NATOShell"))
        {
            scale *= 0.707f;
        }
    }
}