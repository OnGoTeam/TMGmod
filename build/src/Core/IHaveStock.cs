using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IHaveStock : IAmAGun
    {
        bool Stock { get; set; }

        [UsedImplicitly] StateBinding StockBinding { get; }

        [UsedImplicitly] BitBuffer StockBuffer { get; set; }

        float StockState { get; set; }

        [UsedImplicitly] StateBinding StockStateBinding { get; }
        float StockSpeed { get; }
        void UpdateStockStats(float old);

        string StockOn { get; }
        string StockOff { get; }
    }
}
