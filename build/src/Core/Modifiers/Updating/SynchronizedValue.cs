using DuckGame;

namespace TMGmod.Core.Modifiers.Updating
{
    public class SynchronizedValue<T> : Modifier
    {
        public T Value;

        public SynchronizedValue(T value)
        {
            Value = value;
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
}
