using System;
using DuckGame;

namespace TMGmod.Core.Modifiers.Kforce
{
    public sealed class FirstKforce : Modifier
    {
        private readonly Func<float, float> _additionalKforce;
        public uint MaxDelay;
        private uint _currentDelay;

        public FirstKforce(uint maxDelay, Func<float, float> additionalKforce)
        {
            MaxDelay = maxDelay;
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

        protected override void ModifySpent()
        {
            _currentDelay = MaxDelay;
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
