using System;
using System.Collections.Generic;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class Quacking: Modifier
    {
        private readonly BaseGun _target;
        private readonly bool _serverOnly;
        private readonly Action _quacked;

        public Quacking(BaseGun target, bool serverOnly, Action quacked)
        {
            _target = target;
            _serverOnly = serverOnly;
            _quacked = quacked;
        }

        protected override void ModifyUpdate()
        {
            if ((!_serverOnly || _target.isServerForObject) && _target.Quacked())
                _quacked();
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return "Has Quack Action";
        }
    }
}
