using System;
using System.Linq;

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
    }
}
