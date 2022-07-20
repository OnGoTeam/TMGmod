using System;
using DuckGame;

namespace TMGmod.Core.Modifiers
{
    public class Modifier : IModifyEverything
    {
        protected virtual void ModifySpent()
        {
        }

        public void ModifySpent(Action spent)
        {
            ModifySpent();
            spent();
        }

        protected virtual void ModifyUpdate()
        {
        }

        public void ModifyUpdate(Action update)
        {
            ModifyUpdate();
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

        protected virtual void Read(BitBuffer buffer)
        {
        }

        public void Read(BitBuffer buffer, Action read)
        {
            Read(buffer);
            read();
        }

        protected virtual void Write(BitBuffer buffer)
        {
        }

        public void Write(BitBuffer buffer, Action write)
        {
            Write(buffer);
            write();
        }
    }
}
