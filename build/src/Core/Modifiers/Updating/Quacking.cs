using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class Quacking: Modifier
    {
        private readonly BaseGun _target;
        private readonly bool _serverOnly;
        private readonly bool _unQuack;
        private readonly Action _quacked;
        private readonly string _hint;
        private readonly Func<Vec2> _hintOffset;

        public Quacking(BaseGun target, bool serverOnly, bool unQuack, Action quacked, string hint=null, Func<Vec2> hintOffset=default)
        {
            _target = target;
            _serverOnly = serverOnly;
            _unQuack = unQuack;
            _quacked = quacked;
            _hint = hint;
            _hintOffset = hintOffset ?? (() => Vec2.Zero);
        }

        protected override void ModifyUpdate()
        {
            if (!_target.Quacked()) return;
            if (_hint != null)
                _target.Hint(_hint, _hintOffset, "QUACK");
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
