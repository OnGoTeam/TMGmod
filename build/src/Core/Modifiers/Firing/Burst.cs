using System;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Firing
{
    public class Burst : Modifier
    {
        public int Num { private get; set; } = 2;
        public float Wait { private get; set; } = .1f;
        public bool Enabled { private get; set; }
        private int _shotsLeft;
        private readonly BaseGun _target;
        private bool _withinContext;

        public Burst(BaseGun target)
        {
            _target = target;
        }

        public override void ModifyFire(Action fire)
        {
            if (_shotsLeft <= 0)
            {
                fire();
                if (Enabled)
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

        protected override void ModifyUpdate()
        {
            if (_shotsLeft <= 0) return;
            _withinContext = true;
            _target.Fire();
            _withinContext = false;
        }

        protected override void Read(BitBuffer buffer)
        {
            Enabled = buffer.ReadBool();
            _shotsLeft = buffer.ReadInt();
        }

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(Enabled);
            buffer.Write(_shotsLeft);
        }
    }
}
