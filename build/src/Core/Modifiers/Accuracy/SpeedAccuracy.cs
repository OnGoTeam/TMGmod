﻿using System;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Accuracy
{
    public class SpeedAccuracy : Modifier
    {
        private readonly float _horizontal;
        private readonly BaseGun _target;
        private readonly float _threshold;
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
                  +
                  _threshold
                  -
                  (
                      Math.Abs(_target.duck.hSpeed) * _horizontal
                      +
                      Math.Abs(_target.duck.vSpeed) * _vertical
                  )
                : accuracy;
        }
    }
}