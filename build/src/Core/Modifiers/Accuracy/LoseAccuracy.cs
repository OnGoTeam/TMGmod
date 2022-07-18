using System;
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

        public override void ModifyFire(Action fire)
        {
            _drained = Math.Max(Max, _drained + Drain);
            fire();
        }

        public override void ModifyUpdate(Action update)
        {
            _drained = Math.Max(0f, _drained - Regen);
            update();
        }

        public override void Read(BitBuffer buffer, Action read)
        {
            _drained = buffer.ReadFloat();
            read();
        }

        public override void Write(BitBuffer buffer, Action write)
        {
            write();
            buffer.Write(_drained);
        }
    }
}
