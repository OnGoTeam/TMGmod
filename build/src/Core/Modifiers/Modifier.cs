using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.Modifiers.Pipelining;

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

        public virtual void ModifyFire(Action fire)
        {
            fire();
        }

        public virtual bool CanFire()
        {
            return true;
        }

        protected virtual IEnumerable<string> Characteristics()
        {
            yield break;
        }

        public virtual T ModifyPipeline<T>(T pipeline) where T : IPipeline
        {
            return pipeline switch
            {
                ICharacteristicsPipeline<T> characteristics => characteristics.With(Characteristics()),
                _ => pipeline,
            };
        }

        protected static float SafeDiv(float a, float b) => a / Math.Max(.001f, b);
    }
}
