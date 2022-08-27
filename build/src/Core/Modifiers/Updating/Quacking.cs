using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class Quacking : Modifier
    {
        private readonly BaseGun _target;
        private readonly bool _serverOnly;
        private readonly bool _unQuack;
        private readonly Action _quacked;
        private readonly string _hint;
        private readonly Func<bool> _active;
        private readonly Func<Vec2> _hintOffset;

        public Quacking(
            BaseGun target, bool serverOnly, bool unQuack, Action quacked, string hint = null,
            Func<Vec2> hintOffset = default, Func<bool> active = default
        )
        {
            _target = target;
            _serverOnly = serverOnly;
            _unQuack = unQuack;
            _quacked = quacked;
            _hint = hint;
            _active = active ?? (() => true);
            _hintOffset = hintOffset ?? (() => Vec2.Zero);
        }

        protected override void ModifyUpdate()
        {
            if (!_active())
                return;
            _target.Hint(_hint, _hintOffset, "QUACK");
            if (!_target.Quacked()) return;
            // else
            if (!_serverOnly || _target.isServerForObject)
                _quacked();
            if (_unQuack)
                _target.UnQuack();
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return _hint is null ? "Has Quack Action" : $"Quack: {_hint}";
        }
    }
}
