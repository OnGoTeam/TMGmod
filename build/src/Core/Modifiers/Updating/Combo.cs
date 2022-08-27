#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class Combo: Modifier
    {
        private readonly BaseGun _target;
        private readonly string _hint;
        private readonly string[] _combo;
        private readonly Action _action;
        private readonly Queue<string> _log = new();

        public Combo(BaseGun target, string hint, Action action, params string[] combo)
        {
            _target = target;
            _hint = hint;
            _combo = combo;
            _action = action;
        }

        private void PopulateQueue()
        {
            foreach (var key in new[] { "UP", "DOWN", "LEFT", "RIGHT", "QUACK", "RAGDOLL" })
                if (_target.duck.inputProfile.Pressed(key))
                    _log.Enqueue(key);
            while (_log.Count > _combo.Length)
                _log.Dequeue();
        }

        private void RunAction()
        {
            _action();
            _log.Clear();
        }

        private void CheckCombo()
        {
            if (_log.SequenceEqual(_combo))
                RunAction();
        }

        private void UpdateQueue()
        {
            PopulateQueue();
            CheckCombo();
        }

        protected override void ModifyUpdate()
        {
            _target.Hint(_hint, () => Vec2.Zero, _combo);
            if (!_target.isServerForObject || _target.duck is null)
                _log.Clear();
            else
                UpdateQueue();
        }
    }
}
#endif
