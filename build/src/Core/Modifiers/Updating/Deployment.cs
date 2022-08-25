using System;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class Deployment : Modifier
    {
        private readonly BaseGun _target;
        private readonly Action _deployed;
        private readonly Action _folded;
        private readonly Func<bool> _deploying;
        private readonly float _speed;
        private readonly Action<DeploymentState, DeploymentState> _update;
        private float _state;

        public Deployment(BaseGun target, Action deployed, Action folded, Func<bool> deploying, float speed, Action<DeploymentState, DeploymentState> update)
        {
            _target = target;
            _deployed = deployed;
            _folded = folded;
            _deploying = deploying;
            _speed = speed;
            _update = update;
        }

        private bool Folded() => _state < .01f;

        private bool Deployed() => _state > .99f;

        private DeploymentState FullState() => new DeploymentState(deployed: Deployed(), folded: Folded(), state: _state);

        private void SetState(float state)
        {
            var old = FullState();
            _state = Maths.Clamp(state, 0f, 1f);
            if (Deployed() && !old.Deployed)
                _deployed();
            if (Folded() && !old.Folded)
                _folded();
            _update(old, FullState());
        }

        private void Delta(float delta) => SetState(_state + delta);

        protected override void ModifyUpdate()
        {
            if (!_target.isServerForObject)
                return;
            Delta(_deploying() ? +_speed : -_speed);
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_state);
        }

        protected override void Read(BitBuffer buffer)
        {
            SetState(buffer.ReadFloat());
        }

        public override bool CanFire() => Folded() || Deployed();
    }
}
