using System;
using System.Collections.Generic;
using System.Linq;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;

namespace TMGmod.Cases
{
    public class SpawnSpec<T> where T : Holdable
    {
        private readonly Func<T> _thing;
        private readonly float _chance;

        private SpawnSpec(Func<T> thing, float chance)
        {
            _thing = thing;
            _chance = chance;
        }
        
        public SpawnSpec<T> Chance(float chance) => new(_thing, chance * _chance);

        public SpawnSpec<T> Decorate(Func<T, T> f) => new(() => f(_thing()), _chance);
        public static SpawnSpec<T> Base(Type t) => new(() => Editor.CreateThing(t) as T, 1f);
        public static SpawnSpec<T> Base() => Base(typeof(T));

        public Holdable Spawn() => Rando.Float(0f, 1f) < _chance ? _thing() : null;

        public static implicit operator SpawnSpec<Holdable>(SpawnSpec<T> spec) =>
            new(() => spec._thing(), spec._chance);

        public T Thing() => _thing();
        public float Chance() => _chance;
    }

    public static class CaseExtensions
    {
        private static T WithSkin<T>(T h, int skin) where T : IHaveSkin
        {
            h.SkinValue = skin;
            return h;
        }

        private static SpawnSpec<T> Skin<T>(this SpawnSpec<T> f, int skin) where T : Holdable, IHaveSkin =>
            f.Decorate(h => WithSkin(h, skin));

        public static SpawnSpec<T> Skin<T>(this SpawnSpec<T> f, BaseColor skin) where T : Holdable, IHaveSkin =>
            f.Skin((int)skin);
    }

    public abstract class BaseCase : Holdable, IPlatform
    {
        private List<SpawnSpec<Holdable>> _thingsDetailed;
        protected SpawnSpec<T> B<T>() where T : Holdable => SpawnSpec<T>.Base().Decorate(Decorated);
        protected SpawnSpec<Holdable> B(Type t) => SpawnSpec<Holdable>.Base(t).Decorate(Decorated);

        protected BaseCase(float xval, float yval) : base(xval, yval)
        {
            physicsMaterial = PhysicsMaterial.Metal;
        }

        protected List<SpawnSpec<Holdable>> ThingsDetailed
        {
            set
            {
                var max = Math.Max(value.Select(spec => spec.Chance()).Max(), 1f);
                _thingsDetailed = value.Select(spec => spec.Chance(1f / max)).ToList();
            }
        }

        protected BaseColor CaseColor { private get; set; } = BaseColor.No;

        private Holdable Contained()
        {
            Holdable contained = null;
            while (contained is null)
                contained = _thingsDetailed[Rando.Int(_thingsDetailed.Count - 1)].Spawn();
            return contained;
        }

        private Holdable Spawn()
        {
            var contained = Contained();
            if (Rando.Int(500) == 1 && contained is Gun gun1 && gun1.CanSpawnInfinite())
            {
                gun1.infiniteAmmoVal = true;
                gun1.infinite.value = true;
            }

            contained.x = x;
            contained.y = y;
            Level.Add(contained);
            return contained;
        }

        private void HandleDuckIfNecessary(Holdable contained)
        {
            var d = duck;
            if (d is null) return;
            // else
            d.profile.stats.presentsOpened++;
            duck.ThrowItem();
            d.GiveHoldable(contained);
            d.resetAction = true;
            SFX.Play(GetPath("sounds/case_opening"));
        }

        public override void OnPressAction()
        {
            if (owner is null or MagnetGun) return;
            //else
            Level.Remove(this);
            var contained = Spawn();
            HandleDuckIfNecessary(contained);
        }

        private T Decorated<T>(T thing) where T : Holdable
        {
            Spawned(thing);
            return thing;
        }

        protected virtual void Spawned(Holdable thing)
        {
            switch (thing)
            {
                case IHaveSkin skinThing:
                    skinThing.SkinValue = (int)CaseColor;
                    break;
            }
        }

        public override ContextMenu GetContextMenu()
        {
            var menu = base.GetContextMenu();
            menu.AddItem(new ContextChanceRender(_thingsDetailed));
            return menu;
        }
    }
}
