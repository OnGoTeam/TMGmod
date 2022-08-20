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

        public SpawnSpec<T> Chance(float chance) => new SpawnSpec<T>(_thing, chance * _chance);

        public SpawnSpec<T> Decorate(Func<T, T> f) => new SpawnSpec<T>(() => f(_thing()), _chance);
        public static SpawnSpec<T> Base(Type t) => new SpawnSpec<T>(() => Editor.CreateThing(t) as T, 1f);
        public static SpawnSpec<T> Base() => Base(typeof(T));

        public Holdable Spawn() => Rando.Float(0f, 1f) < _chance ? _thing() : null;

        public static implicit operator SpawnSpec<Holdable>(SpawnSpec<T> spec) =>
            new SpawnSpec<Holdable>(() => spec._thing(), spec._chance);

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
        protected SpawnSpec<T> B<T>() where T : Holdable => SpawnSpec<T>.Base().Decorate(Decorated);
        private SpawnSpec<Holdable> B(Type t) => SpawnSpec<Holdable>.Base(t).Decorate(Decorated);

        protected BaseCase(float xval, float yval) : base(xval, yval)
        {
            physicsMaterial = PhysicsMaterial.Metal;
        }

        protected List<Type> Things
        {
            set => ThingsDetailed = value.Select(B).ToList();
        }

        protected List<SpawnSpec<Holdable>> ThingsDetailed { private get; set; }

        protected BaseColor CaseColor { private get; set; } = BaseColor.No;

        private Holdable Contained()
        {
            Holdable contained = null;
            while (contained == null)
                contained = ThingsDetailed[Rando.Int(ThingsDetailed.Count - 1)].Spawn();
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
            if (d == null) return;
            // else
            d.profile.stats.presentsOpened++;
            duck.ThrowItem();
            d.GiveHoldable(contained);
            d.resetAction = true;
            SFX.Play(GetPath("sounds/case_opening"));
        }

        public override void OnPressAction()
        {
            if (owner == null || owner is MagnetGun) return;
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
            menu.AddItem(new ContextChanceRender(ThingsDetailed));
            return menu;
        }
    }
}
