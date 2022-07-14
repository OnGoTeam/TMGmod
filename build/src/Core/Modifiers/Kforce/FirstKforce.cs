using System;

namespace TMGmod.Core.Modifiers.Kforce
{
    public sealed class FirstKforce : Modifier
    {
        private uint _currentDelay;
        private readonly uint _maxDelay;
        private readonly Func<float, float> _additionalKforce;

        public FirstKforce(uint maxDelay, Func<float, float> additionalKforce)
        {
            _maxDelay = maxDelay;
            _additionalKforce = additionalKforce;
        }

        public override float ModifyKforce(float kforce) => _currentDelay <= 0 ? _additionalKforce(kforce) : kforce;
        public override void ModifyUpdate(Action update)
        {
            if (_currentDelay > 0)
                _currentDelay -= 1;
            update();
        }

        public override void ModifyFire(Action fire)
        {
            _currentDelay = _maxDelay;
            fire();
        }
    }
}
