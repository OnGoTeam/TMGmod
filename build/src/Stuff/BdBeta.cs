using System.Linq;
using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    public class BdBeta : Gun
    {
        public BdBeta(float xpos, float ypos) : base(xpos, ypos)
        {
            ammo = 5;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            graphic = new Sprite(GetPath("Molot"));
        }

        public override void Fire()
        {
        }

        public override void OnPressAction()
        {
            if (ammo <= 0) return;
            //else
            if (receivingPress) return;
            //else
            if (Level.CheckCircleAll<BarricadeBeta>(position, 64f).ToList().Count > 0) return;
            var blocks = Level.CheckLineAll<Block>(position, position + new Vec2(16f * offDir, 0f));
            foreach (var block in blocks)
            {
                Deploy(block.position + new Vec2(0f, -10f));
                ammo--;
                return;
            }
            blocks = Level.CheckLineAll<Block>(position + new Vec2(0, 8f), position + new Vec2(16f * offDir, 0f));
            foreach (var block in blocks)
            {
                Deploy(block.position + new Vec2(0f, -10f));
                ammo--;
                return;
            }
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void Deploy(Vec2 vec2)
        {
            if (duck == null) return;
            for (var i = 0; i < 8; ++i)
            {
                var barricade = new BarricadeBeta(vec2.x, vec2.y - i * 4) {owner = duck};
                Level.Add(barricade);
                Fondle(barricade);
                barricade.responsibleProfile = duck.responsibleProfile;
                barricade.clip.Add(duck);
            }
        }
    }
}