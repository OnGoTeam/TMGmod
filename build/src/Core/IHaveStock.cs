﻿using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public interface IHaveStock
    {
        [UsedImplicitly]
        bool Stock { get; set; }
        [UsedImplicitly]
        StateBinding StockBinding { get; }
        [UsedImplicitly]
        float StockState { get; set; }
        [UsedImplicitly]
        StateBinding StockStateBinding { get; }
    }
}