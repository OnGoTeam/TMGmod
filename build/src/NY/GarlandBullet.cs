using DuckGame;
using TMGmod.Core;

namespace TMGmod.NY
{
    public class GarlandBullet:Bullet, IHaveSkin
    {
        internal int[] PrevFrameId;
        private SpriteMap _sprite;

        public GarlandBullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            PrevFrameId = new[]
            {
                Rando.Int(0, 11),
                Rando.Int(0, 11),
                Rando.Int(0, 11),
                Rando.Int(0, 11)
            };
            _sprite = new SpriteMap("Holiday/Garland", 16, 9);
            _graphic = _sprite;
            FrameId = Rando.Int(0, 11);
            
        }

        public override void Draw()
        {
            foreach (var fid in PrevFrameId)
            {
                
            }
            base.Draw();
        }

        public int FrameId
        {
            get => _sprite._frame;
            set => _sprite._frame = value % 12;
        }
    }
}