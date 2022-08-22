using System;
using TMGmod.Core.WClasses;

namespace TMGmod.Core.Modifiers.Updating
{
    public class Quacking: Modifier
    {
        private readonly BaseGun _target;
        private readonly Action _quacked;

        public Quacking(BaseGun target, Action quacked)
        {
            _target = target;
            _quacked = quacked;
        }

        protected override void ModifyUpdate()
        {
            if (_target.Quacked()) _quacked();
        }
    }
}
