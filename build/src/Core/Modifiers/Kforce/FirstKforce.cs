using System;
using DuckGame;

namespace TMGmod.Core.Modifiers.Kforce
{
    public sealed class FirstKforce : Modifier
    {
        private readonly Func<float, float> _additionalKforce;
        private readonly uint _maxDelay;
        private uint _currentDelay;

        public FirstKforce(uint maxDelay, Func<float, float> additionalKforce)
        {
            _maxDelay = maxDelay;
            _additionalKforce = additionalKforce;
        }

        public override float ModifyKforce(float kforce)
        {
            return _currentDelay <= 0 ? _additionalKforce(kforce) : kforce;
        }

        protected override void ModifyUpdate()
        {
            if (_currentDelay > 0) _currentDelay -= 1;
        }

        protected override void ModifyFire()
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
