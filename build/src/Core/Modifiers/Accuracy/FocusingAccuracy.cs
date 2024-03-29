﻿#if FEATURES_1_2_X
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Accuracy
{
    public class FocusingAccuracy : Modifier
    {
        private readonly BaseGun _target;
        private readonly float _max;
        private readonly float _rate;
        private float _loss;

        public FocusingAccuracy(BaseGun target, float max, float rate)
        {
            _target = target;
            _max = max;
            _rate = rate;
        }

        protected override void ModifySpent() => _loss = _max;

        private bool LossShouldBeMax => _target.duck is null || _target.duck.velocity.length > .1f;

        protected override void ModifyUpdate() =>
            _loss = LossShouldBeMax ? _max : Maths.Clamp(_loss - _rate, 0f, _max);

        protected override void Read(BitBuffer buffer) => _loss = buffer.ReadFloat();

        protected override void Write(BitBuffer buffer) => buffer.Write(_loss);

        public override float ModifyAccuracy(float accuracy)
        {
            if (LossShouldBeMax) _loss = _max;
            return _target.duck is null ? accuracy : accuracy - _loss;
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return "Focuses When Standing Still";
            yield return $"Focusing Delay: {SafeDiv(_max, _rate) / 60f:0.##}s";
        }
    }
}
#endif
