using System;
using DuckGame;

namespace TMGmod.Core.BipodsLogic
{
    public static class BipodsImplementation
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

        public static void UpdateSwitchableBipods(this ISwitchBipods target)
        {
            var gun = target.AsAGun();
            if (gun.duck == null) target.SetBipodsDisabled(false);
            else if (!gun.BipodsQ(true)) target.SetBipodsDisabled(false);
            else if (gun.duck.inputProfile.Pressed("QUACK")) target.SetBipodsDisabled(!target.BipodsDisabled);
            target.UpdateBipods();
            if (gun.duck != null && target.BipodsDeployed())
                gun.Hint("silencer", () => gun.barrelOffset, () => gun.duck.inputProfile.GetTriggerImage("QUACK"));
        }

        public static bool BipodsDeployed(this IHaveBipodState target) => target.BipodsState > .99f;
        public static bool BipodsFolded(this IHaveBipodState target) => target.BipodsState < .01f;

        public static void UpdateBipodsSounds(this IDeployBipods target, float old)
        {
            var gun = target.AsAGun();
            if (gun.isServerForObject && target.BipodsDeployed() && old <= 0.99f)
                SFX.Play(target.BipOn);
            if (gun.isServerForObject && target.BipodsFolded() && old >= 0.01f)
                SFX.Play(target.BipOff);
        }

        public static bool BipodsQ(this Gun gun, bool bypassihb = false)
        {
            var duck = gun.duck;
            if (!bypassihb && gun is IHaveBipods ihb && ihb.BipodsDisabled) return false;
            return !(duck is null) && !gun.raised && (duck.crouch || duck.sliding) && duck.grounded &&
                   Math.Abs(duck.hSpeed) < 0.05f;
        }
    }
}
