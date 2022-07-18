using System;
using DuckGame;

namespace TMGmod.Core.Modifiers
{
    public class Modifier : IModifyEverything
    {
        public virtual void ModifyFire(Action fire)
        {
            fire();
        }

        public virtual void ModifyUpdate(Action update)
        {
            update();
        }

        public virtual float ModifyAccuracy(float accuracy)
        {
            return accuracy;
        }

        public virtual float ModifyKforce(float kforce)
        {
            return kforce;
        }

        public static IModifyEverything Identity()
        {
            return new Modifier();
        }

        public virtual void Read(BitBuffer buffer, Action read)
        {
            read();
        }

        public virtual void Write(BitBuffer buffer, Action write)
        {
            write();
        }
    }
}
