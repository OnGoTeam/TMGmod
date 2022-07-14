using System;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Kforce
{
    public class HSpeedKforce : Modifier
    {
        private readonly BaseGun _target;
        private readonly Predicate<float> _predicate;
        private readonly Func<float, float> _modify;

        public HSpeedKforce(
            BaseGun target, Predicate<float> predicate, Func<float, float> modify
        )
        {
            _target = target;
            _predicate = predicate;
            _modify = modify;
        }

        public override float ModifyKforce(float kforce) =>
            _target.duck != null && _predicate(Math.Abs(_target.duck.hSpeed))
                ? Math.Max(0f, _modify(kforce))
                : kforce;
    }
}
