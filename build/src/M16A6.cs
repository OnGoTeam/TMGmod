#if DEBUG
using DuckGame;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    public class M16A6 : AK47
    {
        private uint _cdstate;
        public bool Bipods
        {
            get => !(duck is null) && (duck.crouch || duck.sliding) && duck.grounded && _cdstate < 100;
            set => _kickForce = value ? 0 : 10;
        }
        public M16A6(float xval, float yval) : base(xval, yval)
        {
        }

        private void UpdCds()
        {
            if (100 < _cdstate && _cdstate < 150) _cdstate = 300;
            if (150 < _cdstate && _cdstate < 200) _cdstate = 0;
            Bipods = Bipods;
        }

        public override void Update()
        {
            if (_cdstate > 0) _cdstate -= 1;
            UpdCds();
            base.Update();
        }

        public override void Fire()
        {
            UpdCds();
            base.Fire();
            if (Bipods && _wait >= _fireWait) _cdstate += 49;
        }
    }
}
#endif