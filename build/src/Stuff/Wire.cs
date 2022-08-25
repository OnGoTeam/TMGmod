using System.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [BaggedProperty("canSpawn", false)]
    [UsedImplicitly]
    public class Wire : PhysicsObject
    {
        private SpriteMap _sprite;

        [UsedImplicitly] public float Hp;

        [UsedImplicitly] public StateBinding HpBinding = new StateBinding(nameof(Hp));

        [UsedImplicitly] public int Teksturka;

        [UsedImplicitly] public StateBinding TexBinding = new StateBinding(nameof(Teksturka));
        private int TotalFrames() => _sprite.texture.width * _sprite.texture.height / (_sprite.width * _sprite.height);

        [UsedImplicitly] public readonly EditorProperty<int> Length;

        private const int PartWidth = 16;

        public Wire(float xpos, float ypos) : base(xpos, ypos)
        {
            Hp = 25f;
            _sprite = new SpriteMap(GetPath("WireYes"), PartWidth, 6); //if ny then NY else _
            _graphic = _sprite;
            Teksturka = Rando.Int(0, TotalFrames() - 1);
            Length = new EditorProperty<int>(3, this, 1, 5);
            UpdateLength();
            throwSpeedMultiplier = 0f;
            thickness = 5f;
        }

        private void UpdateLength()
        {
            _center = new Vec2(PartWidth * Length.value / 2f, 3f);
            _collisionOffset = new Vec2(-PartWidth * Length.value / 2f, -3f);
            _collisionSize = new Vec2(PartWidth * Length.value, 6f);
            _weight = 5f * Length.value;
        }

        public override void EditorPropertyChanged(object property)
        {
            if (Length.value > 5)
                Length.value = 5;
            if (Length.value < 1)
                Length.value = 1;
            UpdateLength();
        }

        public override void Update()
        {
            if (Hp <= 0f)
                Hp = 0f;
            else
            {
                var probablyduck = Level.CheckRectAll<IAmADuck>(
                    position + new Vec2(-PartWidth * Length.value / 2f, -3f),
                    position + new Vec2(PartWidth * Length.value / 2f, 3f)
                );
                foreach (var realyduck in probablyduck)
                {
                    if (!(realyduck is Thing r1)) continue;
                    //else
                    r1.hSpeed *= 1f / (Hp / 26f + 1f);
                    r1.vSpeed *= 1f / (Hp / 10f + 1f);
                }

                var wirelist = Level.CheckRectAll<Wire>(
                    position + new Vec2(-PartWidth * Length.value / 3f, -3f),
                    position + new Vec2(PartWidth * Length.value / 3f, 3f)
                );
                if (
                    wirelist.Any(
                        wire => wire != this && wire.sleeping && wire.Hp >= 1f && wire.position.y > position.y
                    )
                )
                {
                    sleeping = true;
                    vSpeed = 0f;
                    hSpeed *= 0.3f;
                }
                else
                {
                    sleeping = false;
                }
            }

            base.Update();
        }

        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet.ammo.penetration < 10f)
                Damage(bullet.ammo);
            Level.Add(Spark.New(hitPos.x, hitPos.y, bullet.travelDirNormalized * Rando.Float(-1f, 1f)));
            return Hit(bullet, hitPos) && bullet.ammo.penetration < 2.1f;
        }

        private void Damage(AmmoType at)
        {
            if (at.penetration < 1.1f) return;
            Hp -= at.penetration;
            if (!(Hp < 1f)) return;
            //else
            thickness = 0.1f;
            _graphic = _sprite = new SpriteMap(GetPath("WireNot"), PartWidth, 6);
            collisionSize = new Vec2(PartWidth * Length.value, 4f);
        }

        public override void Draw()
        {
            for (var i = 0; i < Length.value; i++)
            {
                _sprite.frame = (Teksturka - i).Modulo(TotalFrames());
                _center.x = PartWidth * (-Length.value / 2f + 1 + i);
                base.Draw();
            }

            _center.x = PartWidth * Length.value / 2f;
        }
    }
}
