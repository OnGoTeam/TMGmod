using System;
using DuckGame;
using TMGmod.Core.StockLogic;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class WithStock : Modifier
    {
        private readonly BaseGun _target;
        private bool _deploying;
        private readonly string _stockOn;
        private readonly string _stockOff;
        private readonly float _speed;
        private readonly Action<DeploymentState, DeploymentState> _update;
        private float _state;

        public WithStock(BaseGun target, bool preDeployed, string stockOn, string stockOff, float speed, Action<DeploymentState> update)
        {
            _target = target;
            _stockOn = stockOn;
            _stockOff = stockOff;
            _speed = speed;
            _deploying = preDeployed;
            if (_deploying)
                _state = 1f;
            _update = (_, full) => update(full);
        }

        public bool Folded() => _state < .01f;

        private bool Deployed() => _state > .99f;

        private void Toggle()
        {
            if (_target.SwitchStockQ() && (_deploying || _target.duck?.grounded == true))
                _deploying = !_deploying;
        }

        public IModifyEverything Switching() =>
            ComposedModifier.Compose(
                this,
                new Quacking(_target, true, true, Toggle, "stock", () => new Vec2(-8f, 0f) - _target._holdOffset)
            );

        private DeploymentState FullState() => new(deployed: Deployed(), folded: Folded(), state: _state);

        private void SetState(float state)
        {
            var old = FullState();
            _state = Maths.Clamp(state, 0f, 1f);
            if (Deployed() && !old.Deployed)
                SFX.Play(_stockOn);
            if (Folded() && !old.Folded)
                SFX.Play(_stockOff);
            _update(old, FullState());
        }

        private void Delta(float delta) => SetState(_state + delta);

        protected override void ModifyUpdate()
        {
            if (!_target.isServerForObject)
                return;
            Delta(_deploying ? +_speed : -_speed);
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_deploying);
            buffer.Write(_state);
        }

        protected override void Read(BitBuffer buffer)
        {
            _deploying = buffer.ReadBool();
            SetState(buffer.ReadFloat());
        }

        public override bool CanFire() => Folded() || Deployed();
    }
}
