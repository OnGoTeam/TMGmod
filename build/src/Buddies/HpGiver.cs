using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Buddies
{
    [UsedImplicitly]
    [EditorGroup("TMG|Misc")]
    public class HpGiver: Thing
    {
        private bool _given;

        public HpGiver()
        {
            _graphic = new Sprite("swirl");
            _center = new Vec2(8f, 8f);
            _collisionSize = new Vec2(16f, 16f);
            _collisionOffset = new Vec2(-8f, -8f);
            _canFlip = false;
            _visibleInGame = false;
        }

        public override void Update()
        {
            base.Update();
            if (!isServerForObject) return;
            if (_given) return;
            foreach (var thing in Level.current.things[typeof(Duck)])
            {
                if (!(thing is Duck duck)) continue;
                var hp = new HpArmor(duck.x, duck.y);
                Level.Add(hp);
                duck.Equip(hp);
                _given = true;
            }
        }
    }
}