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

        public static bool BipodsDeployed(this IHaveBipodState target) => target.BipodsState > .99f;
        public static bool BipodsFolded(this IHaveBipodState target) => target.BipodsState < .01f;

        public static void UpdateBipodsSounds(this IDeployBipods target, float old)
        {
            var gun = target.AsAGun();
            if (gun.isServerForObject && target.BipodsDeployed() && old <= 0.99f)
                target.BipOn.Play();
            if (gun.isServerForObject && target.BipodsFolded() && old >= 0.01f)
                target.BipOff.Play();
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
