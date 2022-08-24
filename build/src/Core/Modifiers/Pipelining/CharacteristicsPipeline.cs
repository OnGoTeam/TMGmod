#if DEBUG
using System;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TMGmod.Core.Modifiers.Pipelining
{
    public class CharacteristicsPipeline : IPipeline, ICharacteristicsPipeline<CharacteristicsPipeline>
    {
        private readonly IEnumerable<string> _characteristics;

        public CharacteristicsPipeline(IEnumerable<string> characteristics)
        {
            _characteristics = characteristics;
        }

        public CharacteristicsPipeline New(IEnumerable<string> characteristics) =>
            new CharacteristicsPipeline(characteristics);

        public IEnumerator<string> GetEnumerator() => _characteristics.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public interface ICharacteristicsPipeline<out T> : IEnumerable<string>
    {
        T New(IEnumerable<string> characteristics);
    }

    public static class CharacteristicsPipelineImplementation
    {
#if DEBUG
        public static T With<T>(this ICharacteristicsPipeline<T> pipeline, string characteristic) =>
            pipeline.New(pipeline.Concat(new[] { characteristic }));
#endif

        public static T With<T>(this ICharacteristicsPipeline<T> pipeline, IEnumerable<string> characteristics) =>
            pipeline.New(pipeline.Concat(characteristics));
#if DEBUG
        public static T Map<T>(
            this ICharacteristicsPipeline<T> pipeline, Func<IEnumerable<string>, IEnumerable<string>> map
        ) =>
            pipeline.New(map(pipeline));
#endif
    }
}
