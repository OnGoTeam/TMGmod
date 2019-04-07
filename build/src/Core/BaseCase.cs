using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    [PublicAPI]
    public abstract class BaseCase:Holdable,IPlatform
    {
        private Type _contains;

        protected List<Type> Things { private get; set; }

        protected int CaseId { private get; set; }

        protected BaseCase(float xval, float yval) : base(xval, yval)
        {

        }

        public override void Initialize()
        {
            _contains = Things[Rando.Int(Things.Count - 1)];
        }

        public override void OnPressAction()
        {
            if (owner == null) return;
            //else
            var o = owner;
            var d = duck;
            if (d != null)
            {
                d.profile.stats.presentsOpened++;
                duck.ThrowItem();
            }
            Level.Remove(this);
            {
                Initialize();
            }
            if (!(Editor.CreateThing(_contains) is Holdable newThing)) return;
            if (Rando.Int(500) == 1 && newThing is Gun gun1 && gun1.CanSpawnInfinite())
            {
                gun1.infiniteAmmoVal = true;
                gun1.infinite.value = true;
            }
            newThing.x = o.x;
            newThing.y = o.y;
            if (newThing is IHaveSkin skinThing)
            {
                skinThing.FrameId = CaseId;
            }
            Level.Add(newThing);
            if (d == null) return;
            //else
            d.GiveHoldable(newThing);
            d.resetAction = true;
        }
    }
}