using System;
using System.Linq;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class SwitchingModes: Modifier
    {
        private readonly BaseGun _target;
        private readonly int[] _ammo;
        private readonly int _modes;
        private readonly Action<int> _update;
        private readonly Action _reset;
        private int _mode;
        
        public SwitchingModes(BaseGun target, int[] ammo, Action<int> update, Action reset)
        {
            _target = target;
            _ammo = ammo;
            _modes = ammo.Length;
            _update = update;
            _reset = reset;
            ResetAmmo();
            Reset();
        }

        private void SetMode(int mode)
        {
            _mode = mode;
            _update(mode);
        }

        private void Reset()
        {
            SetMode(0);
            _reset();
        }

        private void ResetAmmo() => _target.ammo = _ammo.Sum();

        public override void ModifyFire(Action fire)
        {
            _target.ammo = _ammo[_mode];
            fire();
            _ammo[_mode] = _target.ammo;
            ResetAmmo();
        }

        private void SwitchMode()
        {
            SetMode((_mode + 1) % _modes);
            SFX.Play(Mod.GetPath<TMGmod>("sounds/tuduc.wav"));
        }

        protected override void ModifyUpdate()
        {
            if (_target.infiniteAmmoVal) _ammo[0] = 99;
            if (_target.duck is null)
                Reset();
            else if (_target.duck.inputProfile.Pressed("QUACK"))
                SwitchMode();
        }

        protected override void Read(BitBuffer buffer)
        {
            _mode = buffer.ReadInt();
            for (var i = 0; i < _modes; i++) _ammo[_mode] = buffer.ReadInt();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_mode);
            for (var i = 0; i < _modes; i++) buffer.Write(_ammo[_mode]);
        }
    }
}
