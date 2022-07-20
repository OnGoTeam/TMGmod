#if FEATURES_1_2
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Buddies
{
    [UsedImplicitly]
    [EditorGroup("TMG|Misc")]
    public class HpGiver : Thing
    {
        private readonly HashSet<Duck> _given = new HashSet<Duck>();

        [UsedImplicitly] public EditorProperty<float> Hp;

        public HpGiver()
        {
            Hp = new EditorProperty<float>(99f, this, 49f, 9999f, 10f);
            _graphic = new Sprite("swirl");
            _center = new Vec2(8f, 8f);
            _collisionSize = new Vec2(16f, 16f);
            _collisionOffset = new Vec2(-8f, -8f);
            _canFlip = false;
            _visibleInGame = false;
            // serverOnly = true;
        }

        public override void Update()
        {
            base.Update();
            if (!isServerForObject) return;
            foreach (var thing in Level.current.things[typeof(Duck)])
            {
                if (!(thing is Duck duck)) continue;
                if (!duck.isServerForObject) continue;
                if (_given.Contains(duck)) continue;
                var hp = new HpArmor(duck.x, duck.y, Hp.value);
                Level.Add(hp);
                // duck.Equip(hp, default, true);
                duck.Equip(hp);
                _given.Add(duck);
            }
        }
    }
}
#endif
