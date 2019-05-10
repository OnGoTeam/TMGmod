using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    /// <summary>
    /// Interface for things which
    /// </summary>
    [PublicAPI]
    public interface IHaveSkin
    {
        /// <summary>
        /// this's sprite's _frame
        /// </summary>
        int FrameId { set; }
        /// <summary>
        /// FrameId syncing
        /// </summary>
        StateBinding FrameIdBinding { get; }
        /// <summary>
        /// Skin Editor Property
        /// </summary>
        EditorProperty<int> Skin { get; }
    }
}