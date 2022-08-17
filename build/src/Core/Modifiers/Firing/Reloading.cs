using System;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Firing
{
    public class Reloading : Modifier
    {
        private readonly BaseGun _target;
        private readonly int _magSize;
        private int _mags;
        private readonly Action<Action<Action<int>>, int> _reload;
        private bool _triggerHeld;

        public Reloading(BaseGun target, int magSize, int mags, Action<Action<Action<int>>, int> reload)
        {
            _target = target;
            _magSize = magSize;
            _mags = mags;
            _target.ammo = (mags + 1) * magSize;
            _reload = reload;
        }

        private void Load(Action<int> success)
        {
            if (_mags <= 0) return;
            // else
            --_mags;
            success(_mags);
        }

        private void TryReload()
        {
            _target.Hint("reload", () => _target.barrelOffset, "SHOOT");
            _reload(Load, _mags);
        }

        public override void ModifyFire(Action fire)
        {
            _target.ammo -= _mags * _magSize;
            fire();
            _target.ammo += _mags * _magSize;
        }

        protected override void ModifyUpdate()
        {
            if (
                _target.ammo <= _mags * _magSize && _mags > 0 && _target._wait <= 0f && _target.action && !_triggerHeld
            )
                TryReload();
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
    }
}
