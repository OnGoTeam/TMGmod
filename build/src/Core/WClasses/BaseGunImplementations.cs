using System;
using System.Linq;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public static class BaseGunImplementations
    {
        public static BitBuffer GetBipodBuffer(this IHaveBipods target)
        {
            var buffer = new BitBuffer();
            buffer.Write(target.Bipods);
            return buffer;
        }

        public static void SetBipodBuffer(this IHaveBipods target, BitBuffer buffer)
        {
            target.Bipods = buffer.ReadBool();
        }

        public static void UpdateBipods(this IHaveBipods target)
        {
            target.Bipods = target.Bipods;
        }

        public static void SetBipods<T>(this T target, bool bipods) where T : Thing, IHaveBipodState
        {
            var old = target.BipodsState;
            if (target.isServerForObject)
                target.BipodsState += target.BipodSpeed * (bipods ? 1 : -1);
            target.UpdateBipodsStats(old);
        }

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

        public static void UpdateSwitchableBipods(this ICanDisableBipods target)
        {
            var gun = target.AsAGun();
            if (gun.duck == null) target.SetBipodsDisabled(false);
            else if (!gun.BipodsQ(true)) target.SetBipodsDisabled(false);
            else if (gun.duck.inputProfile.Pressed("QUACK")) target.SetBipodsDisabled(!target.BipodsDisabled);
            target.UpdateBipods();
        }
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

        public static bool BipodsDeployed(this IHaveBipodState target) => target.BipodsState > .99f;
        public static bool BipodsFolded(this IHaveBipodState target) => target.BipodsState < .01f;

        public static void UpdateBipodsSounds(this IDeployBipods target, float old)
        {
            var gun = target.AsAGun();
            if (gun.isServerForObject && target.BipodsDeployed() && old <= 0.99f)
                NetSoundEffect.Play(target.BipOn);
            if (gun.isServerForObject && target.BipodsFolded() && old >= 0.01f)
                NetSoundEffect.Play(target.BipOff);
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

        public static bool BipodsQ(this Gun gun, bool bypassihb = false)
        {
            var duck = gun.duck;
            if (!bypassihb && gun is IHaveBipods ihb && ihb.BipodsDisabled) return false;
            return !(duck is null) && !gun.raised && (duck.crouch || duck.sliding) && duck.grounded &&
                   Math.Abs(duck.hSpeed) < 0.05f;
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
