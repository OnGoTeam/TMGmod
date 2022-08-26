#if DEBUG
using System;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Firing
{
    public class Pressing : Modifier
    {
        private readonly BaseGun _target;
        private readonly Action _pressed;
        private bool _triggerHeld;

        public Pressing(BaseGun target, Action pressed)
        {
            _target = target;
            _pressed = pressed;
        }

        protected override void ModifyUpdate()
        {
            if (_target.isServerForObject && !_triggerHeld && _target.action)
                _pressed();
            _triggerHeld = _target.action;
        }

        protected override void Read(BitBuffer buffer)
        {
            _triggerHeld = buffer.ReadBool();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_triggerHeld);
        }
    }
}
#endif
