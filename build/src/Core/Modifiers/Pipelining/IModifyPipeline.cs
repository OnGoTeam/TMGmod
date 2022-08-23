namespace TMGmod.Core.Modifiers.Pipelining
{
    public interface IModifyPipeline
    {
        T ModifyPipeline<T>(T pipeline) where T : IPipeline;
    }
}
