using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    public class SnowMgun : BaseSmg
    {
        [UsedImplicitly]
        public SnowMgun(float xval, float yval) : base(xval, yval)
        {
            ammo = 40;
            SetAmmoType<ATSneg>();
            _type = "gun";
            NonSkinFrames = 4;
            SkinFrames = 1;
            Smap = new SpriteMap(GetPath("Holiday/SnowMachineGun"), 17, 9);
            _center = new Vec2(10f, 5f);
            _collisionOffset = new Vec2(-10f, -5f);
            _collisionSize = new Vec2(19f, 9f);
            _barrelOffsetTL = new Vec2(17f, 2f);
            _holdOffset = new Vec2(-2f, 1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.3f;
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.32f;
            _editorName = "SnowMacnineGun";
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
        }

        public override void Update()
        {
            NonSkin = ammo switch
            {
                > 30 => 0,
                > 20 => 1,
                > 10 => 2,
                _ => 3,
            };
            base.Update();
        }
    }
}
