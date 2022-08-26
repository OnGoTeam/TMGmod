using System;
using DuckGame;

namespace TMGmod.Core.Modifiers.Syncing
{
    public class SynchronizedProperty<T> : Modifier
    {
        private readonly Func<T> _get;
        private readonly Action<T, T> _update;
        private readonly Func<T, T> _filter;

        public SynchronizedProperty(Func<T> get, Action<T, T> update, Func<T, T> filter=default)
        {
            _get = get;
            _update = update;
            _filter = filter ?? (value => value);
        }

        private void SetValue(T value)
        {
            var old = Value;
            _update(old, _filter(value));
        }
#if DEBUG
        public void Transact(Func<T, T> transact)
        {
            Value = transact(Value);
        }
#endif

        public T Value
        {
            get => _get();
            set => SetValue(value);
        }

        protected override void Read(BitBuffer buffer)
        {
            Value = buffer.Read<T>();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(Value);
        }
    }

    public static class SynchronizedPropertyImplementation {
        public static void Flip(this SynchronizedProperty<bool> property)
        {
            property.Value = !property.Value;
        }

        public static void Increment(this SynchronizedProperty<int> property)
        {
            ++property.Value;
        }
    }
}
