using System;

namespace TMGmod.Core.Modifiers
{
    public class Modifier : IModifyEverything
    {
        public virtual void ModifyFire(Action fire) => fire();
        public virtual void ModifyUpdate(Action update) => update();
        public virtual float ModifyAccuracy(float accuracy) => accuracy;
        public virtual float ModifyKforce(float kforce) => kforce;
        public static IModifyEverything Identity() => new Modifier();
    }
}
