using DuckGame;

namespace TMGmod.Core.SkinLogic
{
    public interface IShowSkins: IHaveAllowedSkins
    {
        SpriteMap SpriteBase { get; }
    }
}
