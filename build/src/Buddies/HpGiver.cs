#if FEATURES_1_2
using DuckGame;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace TMGmod.Buddies
{
    [UsedImplicitly]
    [EditorGroup("TMG|Misc")]
    public class HpGiver : Thing
    {
        private readonly HashSet<Duck> _given = new HashSet<Duck>();

        public HpGiver()
        {
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
            foreach (var thing in Level.current.things[typeof(Duck)])
            {
                if (!(thing is Duck duck)) continue;
                if (!duck.isServerForObject) continue;
                if (_given.Contains(duck)) continue;
                var hp = new HpArmor(duck.x, duck.y);
                Level.Add(hp);
                // duck.Equip(hp, default, true);
                duck.Equip(hp);
                _given.Add(duck);
            }
        }
    }
}
#endif
