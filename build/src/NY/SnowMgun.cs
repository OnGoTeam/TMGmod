using DuckGame;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    public class SnowMgun : BaseSmg
    {
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
            if (ammo > 20 && ammo <= 30) NonSkin = 1;
            if (ammo > 10 && ammo <= 20) NonSkin = 2;
            if (ammo > 0 && ammo <= 10) NonSkin = 3;
            base.Update();
        }
    }
}
