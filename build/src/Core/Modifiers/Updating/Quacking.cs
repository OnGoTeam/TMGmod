using System;
using System.Collections.Generic;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class Quacking: Modifier
    {
        private readonly BaseGun _target;
        private readonly bool _serverOnly;
        private readonly bool _unQuack;
        private readonly Action _quacked;

        public Quacking(BaseGun target, bool serverOnly, bool unQuack, Action quacked)
        {
            _target = target;
            _serverOnly = serverOnly;
            _unQuack = unQuack;
            _quacked = quacked;
        }

        protected override void ModifyUpdate()
        {
            if (!_target.Quacked()) return;
            // else
            if (!_serverOnly || _target.isServerForObject)
                _quacked();
            if (_unQuack)
                _target.UnQuack();
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return "Has Quack Action";
        }
    }
}
