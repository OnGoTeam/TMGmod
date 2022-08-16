using System;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Firing
{
    public class BifurcatedFw : Modifier
    {
        private readonly BaseGun _target;
        private readonly float _fwc;
        private readonly float _regen;
        private readonly float _drain;
        private readonly float _fwm;
        private float _fw;
        private float _acc;

        public BifurcatedFw(BaseGun target, float fwc, float regen, float drain)
        {
            _target = target;
            var fw0 = target._fireWait;
            _fw = fw0;
            _fwc = fwc;
            _regen = regen;
            _drain = drain;
            _fwm = fw0 - fwc * 2f / 3f;
        }

        protected override void ModifyUpdate()
        {
            _acc -= _regen;
            _acc = Maths.Clamp(_acc, 0f, 1f);
            _target._fireWait = _fw;
        }

        protected override void ModifySpent()
        {
            var waitbase = (_fw - _fwm) / _fwc;
            waitbase = Maths.Clamp(waitbase, .0001f, .9999f);
            var accbase = _acc;
            accbase = Maths.Clamp(accbase, 0f, 1f);
            accbase = (float)Math.Sqrt(accbase);
            accbase = Maths.Clamp(accbase, 0f, 1f);
            var bifurcation = 3 + accbase * .6785728f;
            bifurcation = Maths.Clamp(bifurcation, 1f, 4f);
            waitbase = bifurcation * waitbase * (1 - waitbase);
            waitbase = Maths.Clamp(waitbase, 0f, 1f);
            _target._fireWait = _fw = waitbase * _fwc + _fwm;
            _acc += _drain;
            _acc = Maths.Clamp(_acc, 0f, 1f);
        }

        protected override void Read(BitBuffer buffer)
        {
            _acc = buffer.ReadFloat();
            _fw = buffer.ReadFloat();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_acc);
            buffer.Write(_fw);
        }
    }
}
