using System;
using System.Collections.Generic;
using DuckGame;

namespace TMGmod.Core.Modifiers.Accuracy
{
    public class LoseAccuracy: Modifier
    {
        private float _drained;
        public float Drain;
        public float Regen;
        public float Max;

        public LoseAccuracy(
            float drain, float regen, float max
        )
        {
            Drain = drain;
            Regen = regen;
            Max = max;
        }

        public override float ModifyAccuracy(float accuracy)
        {
            return accuracy - Maths.Clamp(_drained, 0f, Max);
        }

        protected override void ModifySpent()
        {
            _drained = Math.Min(Max, _drained + Drain);
        }

        protected override void ModifyUpdate()
        {
            _drained = Math.Max(0f, _drained - Regen);
        }

        protected override void Read(BitBuffer buffer)
        {
            _drained = buffer.ReadFloat();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_drained);
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return "Loses Accuracy After Each Shot";
            yield return $"Time To Regenerate: {SafeDiv(Max, Regen) / 60f:0.##}s";
        }
    }
}
