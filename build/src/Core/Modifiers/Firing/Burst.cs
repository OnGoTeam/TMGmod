﻿using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Firing
{
    public class Burst : Modifier
    {
        private readonly int _num;
        private readonly float _wait;
        private bool _enabled;
        private int _shotsLeft;
        private readonly BaseGun _target;
        private bool _withinContext;
        private readonly Action<bool> _onSwitch;

        public Burst(BaseGun target, bool enabled, int num, float wait, Action<bool> onSwitch=null)
        {
            _target = target;
            _onSwitch = onSwitch;
            _num = num;
            _wait = wait;
            SetEnabled(enabled);
        }

        private void SetEnabled(bool enabled)
        {
            _enabled = enabled;
            _onSwitch?.Invoke(enabled);
        }

        public void Switch(Func<bool, bool> map) => SetEnabled(map(_enabled));

        public override void ModifyFire(Action fire)
        {
            if (_target.receivingPress)
            {
                if (_target.loaded)
                    fire();
                return;
            }
            if (!_target.isServerForObject)
                return;
            if (_shotsLeft <= 0 && _target._wait <= 0)
            {
                fire();
                if (!_enabled) return;
                _shotsLeft = _num - 1;
                if (_shotsLeft > 0)
                    _target._wait = _wait;
            }
            else if (_withinContext)
            {
                fire();
                --_shotsLeft;
                if (_shotsLeft > 0)
                    _target._wait = _wait;
            }
        }

        private void ShootLeft()
        {
            _withinContext = true;
            _target.ForeignFire();
            _withinContext = false;
        }

        private void UpdateShotsLeft()
        {
            if (_shotsLeft > 0 && _target._wait <= 0f && _target.isServerForObject) ShootLeft();
        }

        private void DoSwitch()
        {
            Switch(burst => !burst);
            SFX.Play(Mod.GetPath<TMGmod>("sounds/tuduc.wav"));
        }

        protected override void ModifyUpdate()
        {
            UpdateShotsLeft();
        }

        public IModifyEverything SwichingOnQuack()
        {
            return ComposedModifier.Compose(
                new Quacking(_target, true, true, DoSwitch, "burst"),
                this
            );
        }

        protected override void Read(BitBuffer buffer)
        {
            SetEnabled(buffer.ReadBool());
            _shotsLeft = buffer.ReadInt();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_enabled);
            buffer.Write(_shotsLeft);
        }

        protected override IEnumerable<string> Characteristics()
        {
            yield return $"Burst: {_num}";
        }
    }
}
