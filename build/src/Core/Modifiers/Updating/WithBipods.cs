using System;
using DuckGame;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class WithBipods : Modifier
    {
        private readonly BaseGun _target;
        private readonly string _bipOn;
        private readonly string _bipOff;
        private readonly float _speed;
        private readonly Action<DeploymentState, DeploymentState> _update;
        private bool _disabled;
        private float _state;

        public WithBipods(BaseGun target, string bipOn, string bipOff, float speed, Action<DeploymentState> update)
        {
            _target = target;
            _bipOn = bipOn;
            _bipOff = bipOff;
            _speed = speed;
            _update = (_, full) => update(full);
        }

        private bool Folded() => _state < .01f;

        public bool Deployed() => _state > .99f;

        private void Toggle()
        {
            if (_disabled || Deployable())
                _disabled = !_disabled;
        }

        public IModifyEverything Disableable(Vec2 offset) =>
            ComposedModifier.Compose(
                this,
                new Quacking(_target, true, false, Toggle, "bipods", () => offset, active: Deployable)
            );

        private bool Deployable() => _target.BipodsQ();

        private bool Deploying() => Deployable() && !_disabled;

        private DeploymentState FullState() => new DeploymentState(deployed: Deployed(), folded: Folded(), state: _state);

        private void SetState(float state)
        {
            var old = FullState();
            _state = Maths.Clamp(state, 0f, 1f);
            if (Deployed() && !old.Deployed)
                SFX.Play(_bipOn);
            if (Folded() && !old.Folded)
                SFX.Play(_bipOff);
            _update(old, FullState());
        }

        private void Delta(float delta) => SetState(_state + delta);

        protected override void ModifyUpdate()
        {
            if (!_target.isServerForObject)
                return;
            if (!Deployable())
                _disabled = false;
            Delta(Deploying() ? +_speed : -_speed);
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_disabled);
            buffer.Write(_state);
        }

        protected override void Read(BitBuffer buffer)
        {
            _disabled = buffer.ReadBool();
            SetState(buffer.ReadFloat());
        }

        public override bool CanFire() => Folded() || Deployed();
    }
}
