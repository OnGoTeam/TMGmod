using System;
using System.Collections.Generic;
using DuckGame;

namespace TMGmod.Core.Modifiers.Firing
{
    public class Charging: Modifier
    {
        private readonly Holdable _target;
        private readonly Action<int, int> _update;
        private readonly Action<int> _release;
        private int _counter;

        public Charging(Holdable target, Action<int, int> update, Action<int> release)
        {
            _target = target;
            _update = update;
            _release = release;
            update(0, 0);
        }

        private int NextCounterValue()
        {
            if (_target.action)
                return _counter + 1;
            if (_counter > 0)
                _release(_counter);
            return 0;
        }

        private void SetCounterValue(int newCounter)
        {
            if (newCounter != _counter)
                _update(newCounter, _counter);
            _counter = newCounter;
        }

        private void UpdateCounter()
        {
            SetCounterValue(NextCounterValue());
        }
        protected override void ModifyUpdate()
        {
            if (_target.isServerForObject) UpdateCounter();
        }

        protected override void Read(BitBuffer buffer) {
            SetCounterValue(buffer.ReadInt());
        }

        protected override void Write(BitBuffer buffer) => buffer.Write(_counter);

        protected override IEnumerable<string> Characteristics()
        {
            yield return "Charges By Holding Action";
        }
    }
}
