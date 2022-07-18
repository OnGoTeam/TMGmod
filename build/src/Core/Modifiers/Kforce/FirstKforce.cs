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

        public override void ModifyUpdate(Action update)
        {
            if (_currentDelay > 0) _currentDelay -= 1;
            update();
        }

        public override void ModifyFire(Action fire)
        {
            _currentDelay = _maxDelay;
            fire();
        }

        public override void Read(BitBuffer buffer, Action read)
        {
            _currentDelay = buffer.ReadUInt();
            read();
        }

        public override void Write(BitBuffer buffer, Action write)
        {
            write();
            buffer.Write(_currentDelay);
        }
    }
}
