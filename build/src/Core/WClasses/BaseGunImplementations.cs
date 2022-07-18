using System;
using System.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.StockLogic;

namespace TMGmod.Core.WClasses
{
    public static class BaseGunImplementations
    {

        public static void SetStock<T>(this T target, bool stock) where T : Thing, IHaveStock
        {
            var old = target.StockState;
            if (target.isServerForObject)
                target.StockState += target.StockSpeed * (stock ? 1 : -1);
            target.UpdateStockStats(old);
        }

        private static int FilterSkin(this IHaveAllowedSkins target, int skin) =>
            target.AllowedSkins.Contains(skin)
                ? skin
                : Rando.ChooseInt(target.AllowedSkins.ToArray());

        public static void UpdateSkin(this IHaveAllowedSkins target) =>
            target.FrameId = target.FilterSkin(target.Skin.value);
        public static BitBuffer GetStockBuffer(this IHaveStock target)
        {
            var buffer = new BitBuffer();
            buffer.Write(target.Stock);
            return buffer;
        }

        public static void SetStockBuffer(this IHaveStock target, BitBuffer buffer)
        {
            target.Stock = buffer.ReadBool();
        }

        public static void UpdateStock(this IHaveStock target)
        {
            var gun = target.AsAGun();
            if (gun.SwitchStockQ() && (target.Stock || gun.duck.grounded) && gun.duck.inputProfile.Pressed("QUACK"))
            {
                target.Stock = !target.Stock;

                SFX.Play("quack", -1, gun.duck.quackPitch);
            }
            else if (gun.duck != null)
            {
                target.Stock = target.Stock;
            }
        }

        public static bool StockDeployed(this IHaveStock target) => target.StockState > .99f;
        public static bool StockFolded(this IHaveStock target) => target.StockState < .01f;

        public static void UpdateStockSounds(this IHaveStock target, float old)
        {
            var gun = target.AsAGun();
            if (gun.isServerForObject && target.StockDeployed() && old <= 0.99f)
                SFX.Play(target.StockOn);
            if (gun.isServerForObject && target.StockFolded() && old >= 0.01f)
                SFX.Play(target.StockOff);
        }

        public static bool HandleQ(this Gun gun)
        {
            var duck = gun.duck;
            return !(duck is null) && !gun.raised && duck.sliding && duck.grounded && Math.Abs(duck.hSpeed) < 1f;
        }

        [UsedImplicitly]
        public static bool SwitchStockQ(this Gun gun)
        {
            var duck = gun.duck;
            return !(duck is null) && !duck.sliding;
        }
    }
}
