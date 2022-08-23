using System;
using System.Collections.Generic;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Accuracy
{
    public class SpeedAccuracy : Modifier
    {
        private readonly BaseGun _target;
        private readonly float _threshold;
        private readonly float _horizontal;
        private readonly float _vertical;

        public SpeedAccuracy(
            BaseGun target, float threshold, float horizontal, float vertical
        )
        {
            _target = target;
            _threshold = threshold;
            _horizontal = horizontal;
            _vertical = vertical;
        }

        public override float ModifyAccuracy(float accuracy)
        {
            return _target.duck != null
                ? accuracy
                  -
                  Math.Max(
                      0f,
                      Math.Abs(_target.duck.hSpeed) * _horizontal
                      +
                      Math.Abs(_target.duck.vSpeed) * _vertical
                      -
                      _threshold
                  )
                : accuracy;
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return "Decreases Accuracy When Moving";
        }
    }
}
