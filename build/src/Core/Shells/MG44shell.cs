using DuckGame;

namespace TMGmod.Core.Shells
{
    // ReSharper disable once InconsistentNaming
    public class MG44Shell : EjectedShell
    {
        public MG44Shell(float xpos, float ypos)
          : base(xpos, ypos, Mod.GetPath<TMGmod>("MG44Shell"))
        {
        }
        public override void Update()
        {
            base.Update();
            _angle = Maths.DegToRad(-_spinAngle);
        }
    }
}