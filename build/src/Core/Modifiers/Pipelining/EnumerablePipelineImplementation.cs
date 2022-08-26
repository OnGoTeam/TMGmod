using System.Collections.Generic;
using System.Linq;

namespace TMGmod.Core.Modifiers.Pipelining
{
    public static class EnumerablePipelineImplementation
    {
        public static TPipeline With<TPipeline, TElement>(
            this IEnumerablePipeline<TPipeline, TElement> pipeline, IEnumerable<TElement> characteristics
        ) =>
            pipeline.New(pipeline.Concat(characteristics));
    }
}
