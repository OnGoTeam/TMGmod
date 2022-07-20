using System;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Firing
{
    public class Burst : Modifier
    {
        public int Num { private get; set; } = 2;
        public float Wait { private get; set; } = .1f;
        private bool _enabled;
        private int _shotsLeft;
        private readonly BaseGun _target;
        private bool _withinContext;
        public bool SwitchOnQuack { private get; set; }
        public Action<bool> OnSwitch { private get; set; }

        public Burst(BaseGun target, bool enabled)
        {
            _target = target;
            _enabled = enabled;
        }

        private void Switch(Func<bool, bool> map)
        {
            _enabled = map(_enabled);
            OnSwitch?.Invoke(_enabled);
        }

        public override void ModifyFire(Action fire)
        {
            if (_shotsLeft <= 0)
            {
                fire();
                if (_enabled)
                {
                    _shotsLeft = Num - 1;
                    if (_shotsLeft > 0)
                        _target._wait = Wait;
                }
            }
            else if (_withinContext && _target._wait <= 0f)
            {
                fire();
                --_shotsLeft;
                if (_shotsLeft > 0)
                    _target._wait = Wait;
            }
        }

        private void ShootLeft()
        {
            _withinContext = true;
            _target.Fire();
            _withinContext = false;
        }

        private void UpdateShotsLeft()
        {
            if (_shotsLeft > 0) ShootLeft();
        }

        private bool NeedSwitch() => SwitchOnQuack && _target.duck?.inputProfile.Pressed("QUACK") == true;

        private void DoSwitch()
        {
            Switch(burst => !burst);
            SFX.Play(Mod.GetPath<TMGmod>("sounds/tuduc.wav"));
        }

        private void UpdateSwitch()
        {
            if (NeedSwitch()) DoSwitch();
        }

        protected override void ModifyUpdate()
        {
            UpdateSwitch();
            UpdateShotsLeft();
        }

        protected override void Read(BitBuffer buffer)
        {
            _enabled = buffer.ReadBool();
            _shotsLeft = buffer.ReadInt();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_enabled);
            buffer.Write(_shotsLeft);
        }
    }
}
