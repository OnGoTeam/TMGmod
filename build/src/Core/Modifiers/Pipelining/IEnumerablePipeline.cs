using System.Collections.Generic;

namespace TMGmod.Core.Modifiers.Pipelining
{
    public interface IEnumerablePipeline<out TPipeline, TElement> : IEnumerable<TElement>, IPipeline
    {
        TPipeline New(IEnumerable<TElement> elements);
    }
}
