using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod.NY
{
    [EditorGroup("TMG|SMG")]
    public class SnowMgun:BaseSmg
    {
        public SnowMgun(float xval, float yval) : base(xval, yval)
        {
            ammo = 40;
            _ammoType = new AT9mmS
            {
                bulletType = typeof(SnowBullet),
                bulletSpeed = 25f,
                range = 500f,
                accuracy = 0.95f,
                bulletLength = 3f,
                sprite = new Sprite(GetPath("Holiday/snow")),
                affectedByGravity = true
            };
            _graphic = new Sprite(GetPath("AF2011"));
        }
    }
}