﻿using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.BipodsLogic
{
    public interface IHaveBipods
    {
        bool Bipods { get; set; }

        [UsedImplicitly] StateBinding BipodsBinding { get; }

        bool BipodsDisabled { get; }
    }
}
