using System.Linq;
using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    public class BdBeta : Holdable
    {
        private int _ammo;

        public BdBeta(float xpos, float ypos) : base(xpos, ypos)
        {
            _ammo = 5;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            graphic = new Sprite(GetPath("Molot"));
        }

        public override void OnPressAction()
        {
            if (_ammo <= 0) return;
            //else
            if (Level.CheckCircleAll<BarricadeBeta>(position, 64f).ToList().Count > 0) return;
            var blocks = Level.CheckLineAll<Block>(position, position + new Vec2(16f * offDir, 0f));
            foreach (var block in blocks)
            {
                Deploy(block.position + new Vec2(0f, -10f));
                _ammo--;
                return;
            }
            blocks = Level.CheckLineAll<Block>(position + new Vec2(0, 8f), position + new Vec2(16f * offDir, 0f));
            foreach (var block in blocks)
            {
                Deploy(block.position + new Vec2(0f, -10f));
                _ammo--;
                return;
            }
            base.OnPressAction();
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void Deploy(Vec2 vec2)
        {
            for (var i = 0; i < 8; ++i)
            {
                var barricade = new BarricadeBeta(vec2.x, vec2.y - i * 4);
                Level.Add(barricade);
                Fondle(barricade);
                if (owner != null)
                    barricade.responsibleProfile = owner.responsibleProfile;
                barricade.clip.Add((MaterialThing) owner);
            }
        }
    }
}