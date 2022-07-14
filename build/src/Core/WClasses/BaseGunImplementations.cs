using DuckGame;

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

        private static void UpdateBipods(this IHaveBipods target)
        {
            target.Bipods = target.Bipods;
        }

        public static void SetBipods<T>(this T target, bool bipods) where T : Thing, IHaveBipodState
        {
            var old = target.BipodsState;
            if (target.isServerForObject)
                target.BipodsState += target.BipodSpeed * (bipods ? 1 : -1);
            target.UpdateStats(old);
        }

        public static void UpdateSkin(this IHaveAllowedSkins target)
        {
            var bublic = target.Skin.value;
            while (!target.AllowedSkins.Contains(bublic)) bublic = Rando.Int(0, 9);
            target.FrameId = bublic;
        }

        public static void UpdateSwitchableBipods(this ICanDisableBipods target)
        {
            var gun = target.AsAGun();
            if (gun.duck == null) target.SetBipodsDisabled(false);
            else if (!BaseGun.BipodsQ(gun, true)) target.SetBipodsDisabled(false);
            else if (gun.duck.inputProfile.Pressed("QUACK")) target.SetBipodsDisabled(!target.BipodsDisabled);
            target.UpdateBipods();
        }
    }
}
