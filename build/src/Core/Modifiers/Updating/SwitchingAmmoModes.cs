using System;
using System.Collections.Generic;
using System.Linq;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class SwitchingAmmoModes : Modifier
    {
        private readonly BaseGun _target;
        private readonly int[] _ammo;
        private readonly int _modes;
        private readonly Action<int> _update;
        private readonly Action _reset;
        private int _mode;

        public SwitchingAmmoModes(BaseGun target, int[] ammo, Action<int> update, Action reset)
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
            _mode = mode.Modulo(_modes);
            _update(_mode);
        }

        private void ResetAmmo() => _target.ammo = _ammo.Sum();

        public override void ModifyFire(Action fire)
        {
            _target.ammo = _ammo[_mode];
            fire();
            _ammo[_mode] = _target.ammo;
            ResetAmmo();
        }

        private void Reset()
        {
            SetMode(0);
            _reset();
        }

        private void SwitchMode()
        {
            SetMode(_mode + 1);
            SFX.Play(Mod.GetPath<TMGmod>("sounds/tuduc.wav"));
        }

        protected override void ModifyUpdate()
        {
            if (_target.infiniteAmmoVal) _ammo[0] = 99;
            if (_target.duck is null)
                Reset();
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

        public IModifyEverything SwitchingOnQuack()
        {
            return ComposedModifier.Compose(this, new Quacking(_target, true, true, SwitchMode));
        }

        private class Proxy<T>
        {
            public T Value;
            public Proxy(T value) => Value = value;
        }

        public IModifyEverything Animated(
            Action<int, int> update,
            Func<int, int> trigger
        )
        {
            var animating = new Proxy<Animating<int>>(null);
            animating.Value = new Animating<int>(
                (frames, mode) =>
                {
                    if (frames > 0 && _target.owner is null)
                    {
                        animating.Value?.Cancel();
                        SetMode(_mode);
                    }
                    else
                        update(frames, mode.Modulo(_modes));
                },
                mode =>
                {
                    SetMode(mode);
                    SFX.Play(Mod.GetPath<TMGmod>("sounds/tuduc.wav"));
                },
                block: true
            );
            return ComposedModifier.Compose(
                this,
                animating.Value,
                new Quacking(
                    _target,
                    true,
                    false,
                    () =>
                    {
                        if (!animating.Value.Active())
                            animating.Value.Set(
                                trigger(_mode),
                                (_mode + 1).Modulo(_modes)
                            );
                    }
                )
            );
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return "Switches Ammo";
        }
    }
}
