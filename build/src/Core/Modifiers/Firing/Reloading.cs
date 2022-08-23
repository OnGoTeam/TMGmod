using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Firing
{
    public class Reloading : Modifier
    {
        private readonly BaseGun _target;
        private readonly int _magSize;
        private readonly Vec2 _magOffset;
        private readonly Action<Action<Action<int>, Action>, int> _reload;
        private int _mags;
        private bool _triggerHeld;

        public Reloading(BaseGun target, int magSize, Vec2 magOffset, Action<Action<Action<int>, Action>, int> reload)
        {
            _target = target;
            _magSize = magSize;
            _reload = reload;
            _magOffset = magOffset;
            _mags = (_target.ammo - 1) / magSize;
        }

        private void Load(Action<int> success, Action fail)
        {
            if (_mags > 0)
                success(--_mags);
            else
                fail();
        }

        public override void ModifyFire(Action fire)
        {
            _target.ammo -= _mags * _magSize;
            fire();
            _target.ammo += _mags * _magSize;
        }

        private void TryReload()
        {
            _reload(Load, _mags);
        }

        private void Hint()
        {
            _target.Hint("reload", () => _magOffset, "SHOOT");
        }

        private void OnEmpty()
        {
            Hint();
            if (_target._wait <= 0f && _target.action && !_triggerHeld)
                TryReload();
        }

        protected override void ModifyUpdate()
        {
            if (
                _target.ammo <= _mags * _magSize
            )
                OnEmpty();
            _triggerHeld = _target.action;
        }

        protected override void Read(BitBuffer buffer)
        {
            _mags = buffer.ReadInt();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_mags);
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return $"Extra Mags: {_mags}";
        }
    }
}
