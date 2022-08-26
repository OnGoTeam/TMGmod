using System.Collections;
using System.Collections.Generic;

namespace TMGmod.Core.Modifiers.Pipelining
{
    public abstract class EnumerablePipeline<TPipeline, TElement> : IEnumerablePipeline<TPipeline, TElement>
    {
        private readonly IEnumerable<TElement> _elements;

        protected EnumerablePipeline(IEnumerable<TElement> elements) => _elements = elements;

        public abstract TPipeline New(IEnumerable<TElement> elements);

        public IEnumerator<TElement> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
