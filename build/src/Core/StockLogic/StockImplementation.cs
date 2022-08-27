using DuckGame;

namespace TMGmod.Core.StockLogic
{
    public static class StockImplementation
    {
        public static bool SwitchStockQ(this Holdable gun)
        {
            var duck = gun.duck;
            return duck is { sliding: false };
        }
    }
}
