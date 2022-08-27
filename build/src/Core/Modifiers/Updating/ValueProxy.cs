namespace TMGmod.Core.Modifiers.Updating
{
    public record ValueProxy<T>
    {
        public T Value;
        public ValueProxy(T value) => Value = value;
    }
}
