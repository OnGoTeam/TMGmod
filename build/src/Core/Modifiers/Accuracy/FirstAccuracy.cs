using System;
using DuckGame;

namespace TMGmod.Core.Modifiers.Accuracy
{
    public sealed class FirstAccuracy : Modifier
    {
        private readonly Func<float, float> _reducedAccuracy;
        private readonly uint _maxDelay;
        private uint _currentDelay;

        public FirstAccuracy(uint maxDelay, Func<float, float> reducedAccuracy)
        {
            _maxDelay = maxDelay;
            _reducedAccuracy = reducedAccuracy;
        }

        public override float ModifyAccuracy(float accuracy)
        {
            return _currentDelay > 0 ? _reducedAccuracy(accuracy) : accuracy;
        }

        protected override void ModifyUpdate()
        {
            if (_currentDelay > 0) _currentDelay -= 1;
        }

        protected override void ModifySpent()
        {
            _currentDelay = _maxDelay;
        }

        protected override void Read(BitBuffer buffer)
        {
            _currentDelay = buffer.ReadUInt();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_currentDelay);
        }
    }
}
