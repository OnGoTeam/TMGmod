using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IHaveSkin : IHaveFrameId
    {
        EditorProperty<int> Skin { get; }
    }

    public interface IHaveFrameId
    {
        int FrameId { set; }

        [UsedImplicitly] StateBinding FrameIdBinding { get; }
    }

    public interface IHaveAllowedSkins : IHaveSkin
    {
        ICollection<int> AllowedSkins { get; }
    }
}
