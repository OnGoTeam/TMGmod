using System.Collections.Generic;

namespace TMGmod.Core.Modifiers.Pipelining
{
    public class CharacteristicsPipeline :
        EnumerablePipeline<CharacteristicsPipeline, string>, ICharacteristicsPipeline<CharacteristicsPipeline>
    {
        public CharacteristicsPipeline(IEnumerable<string> elements) : base(elements)
        {
        }

        public override CharacteristicsPipeline New(IEnumerable<string> elements) => new(elements);
    }

    public interface ICharacteristicsPipeline<out T> : IEnumerablePipeline<T, string>
    {
    }
}
