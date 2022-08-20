namespace TMGmod.Core.SkinLogic
{
    public interface IHaveSkin : IHaveFrameId, ISupportEnablingSkins
    {
        int Skin { get; set; }
    }
}
