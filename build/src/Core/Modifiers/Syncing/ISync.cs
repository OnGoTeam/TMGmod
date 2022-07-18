using System;
using DuckGame;

namespace TMGmod.Core.Modifiers.Syncing
{
    public interface ISync
    {
        void Read(BitBuffer buffer, Action read);
        void Write(BitBuffer buffer, Action write);
    }
}
