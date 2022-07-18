using DuckGame;

namespace TMGmod.Core.SkinLogic
{
    public interface IHaveSkin : IHaveFrameId
    {
        EditorProperty<int> Skin { get; }
    }
}
