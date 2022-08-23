using System;
using System.Collections.Generic;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Kforce
{
    public class HSpeedKforce : Modifier
    {
        private readonly Func<float, float> _modify;
        private readonly Predicate<float> _predicate;
        private readonly BaseGun _target;

        public HSpeedKforce(
            BaseGun target, Predicate<float> predicate, Func<float, float> modify
        )
        {
            _target = target;
            _predicate = predicate;
            _modify = modify;
        }

        public override float ModifyKforce(float kforce)
        {
            return _target.duck != null && _predicate(Math.Abs(_target.duck.hSpeed))
                ? Math.Max(0f, _modify(kforce))
                : kforce;
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return "Increases Kickforce When Moving";
        }
    }
}
