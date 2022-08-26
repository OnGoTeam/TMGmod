using System;
using DuckGame;
using TMGmod.Core.Modifiers.Pipelining;

namespace TMGmod.Core.Modifiers.Updating
{
    public class DynamicModifier: IModifyEverything
    {
        private readonly IModifyEverything _modifier;
        private readonly Func<bool> _active;

        public DynamicModifier(IModifyEverything modifier, Func<bool> active)
        {
            _modifier = modifier;
            _active = active;
        }

        public void ModifySpent(Action spent)
        {
            if (_active())
                _modifier.ModifySpent(spent);
            else
                spent();
        }

        public void ModifyUpdate(Action update)
        {
            if (_active())
                _modifier.ModifyUpdate(update);
            else
                update();
        }

        public float ModifyAccuracy(float accuracy)
        {
            return _active() ? _modifier.ModifyAccuracy(accuracy) : accuracy;
        }

        public float ModifyKforce(float kforce)
        {
            return _active() ? _modifier.ModifyKforce(kforce) : kforce;
        }

        public void Read(BitBuffer buffer, Action read)
        {
            if (buffer.ReadBool())
                _modifier.Read(buffer, read);
            else
                read();
        }

        public void Write(BitBuffer buffer, Action write)
        {
            if (_active())
            {
                buffer.Write(true);
                _modifier.Write(buffer, write);
            }
            else
            {
                buffer.Write(false);
                write();
            }
        }

        public void ModifyFire(Action fire)
        {
            if (_active())
                _modifier.ModifyFire(fire);
            else
                fire();
        }

        public bool CanFire()
        {
            return !_active() || _modifier.CanFire();
        }

        public T ModifyPipeline<T>(T pipeline) where T : IPipeline
        {
            return _active() ? _modifier.ModifyPipeline(pipeline) : pipeline;
        }
    }
}
