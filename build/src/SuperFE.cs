using DuckGame;
using JetBrains.Annotations;

namespace TMGmod
{
    [EditorGroup("TMG|Misc")]
    [PublicAPI]
    public class SuperFe:FireExtinguisher
    {
        public SuperFe(float xval, float yval) : base(xval, yval)
        {
            _kickForce = 12f;
            _editorName = "SFE";
        }

        public override void Update()
        {
            base.Update();
            if (_firing && ammo > 0)
            {
                ApplyKick();
            }
        }
    }
}