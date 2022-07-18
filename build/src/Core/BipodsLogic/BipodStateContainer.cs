using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.BipodsLogic
{
    public class BipodStateContainer
    {
        private float _bipodsState;

        public float Get(BaseGun target) => target.duck != null ? _bipodsState : 0;
        public void Set(float bipodsState) => _bipodsState = Maths.Clamp(bipodsState, 0f, 1f);
    }
}
