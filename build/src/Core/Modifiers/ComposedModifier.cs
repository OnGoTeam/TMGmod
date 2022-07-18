using System;
using System.Linq;
using DuckGame;

namespace TMGmod.Core.Modifiers
{
    public sealed class ComposedModifier : IModifyEverything
    {
        private readonly IModifyEverything _left;
        private readonly IModifyEverything _right;

        private ComposedModifier(IModifyEverything left, IModifyEverything right)
        {
            _left = left;
            _right = right;
        }

        public void ModifyFire(Action fire)
        {
            _left.ModifyFire(() => _right.ModifyFire(fire));
        }

        public void ModifyUpdate(Action update)
        {
            _left.ModifyUpdate(() => _right.ModifyUpdate(update));
        }

        public float ModifyAccuracy(float accuracy)
        {
            return _left.ModifyAccuracy(_right.ModifyAccuracy(accuracy));
        }

        public float ModifyKforce(float kforce)
        {
            return _left.ModifyKforce(_right.ModifyKforce(kforce));
        }

        public static IModifyEverything Compose(params IModifyEverything[] modifiers)
        {
            return modifiers.Reverse().Aggregate(
                Modifier.Identity(),
                (left, right) => new ComposedModifier(left, right)
            );
        }

        public void Read(BitBuffer buffer, Action read)
        {
            _left.Read(buffer, () => _right.Read(buffer, read));
        }

        public void Write(BitBuffer buffer, Action write)
        {
            _left.Write(buffer, () => _right.Write(buffer, write));
        }
    }
}
