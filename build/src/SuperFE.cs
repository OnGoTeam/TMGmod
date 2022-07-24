using DuckGame;
using JetBrains.Annotations;

namespace TMGmod
{
    [EditorGroup("TMG|Misc")]
    [UsedImplicitly]
    public class SuperFe : FireExtinguisher
    {
        public SuperFe(float xval, float yval) : base(xval, yval)
        {
            _editorName = "SFE";
            ammo = 666;
            _kickForce = 2f;
        }

        public override void Update()
        {
            base.Update();
            if (_firing && ammo > 0) ApplyKick();
        }
    }
}
