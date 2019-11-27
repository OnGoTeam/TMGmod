#if DEBUG
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    public class M16A6 : AK47, IHaveBipods
    {
        private uint _cdstate;
        [UsedImplicitly]
        public bool Bipods
        {
            get => BaseGun.BipodsQ(this);
            set => _kickForce = value ? 0 : 10;
        }

        public BitBuffer BipodsBuffer { get; set; }

        public StateBinding BipodsBinding => new StateBinding(nameof(Bipods));
        public bool BipodsDisabled => false;

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