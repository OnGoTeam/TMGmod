using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Misc|Grenadelauncher")]
    public class M72 : Gun
    {

        public M72(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 5;
            _ammoType = new ATGrenade
            {
                range = 2000f,
                accuracy = 0.95f,
                penetration = 1f,
                barrelAngleDegrees = -7.5f,
                bulletSpeed = 10f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("M72a5"));
            center = new Vec2(16f, 5.5f);
            collisionOffset = new Vec2(-16f, -5.5f);
            collisionSize = new Vec2(32f, 11f);
            _barrelOffsetTL = new Vec2(32f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 1f;
            _kickForce = 1.2f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            _editorName = "M72 Grenade Launcher";
            weight = 4.5f;
        }
        public override void Update()
        {
            if (ammo == 4) graphic = new Sprite(GetPath("M72a4"));
            if (ammo == 3) graphic = new Sprite(GetPath("M72a3"));
            if (ammo == 2) graphic = new Sprite(GetPath("M72a2"));
            if (ammo == 1) graphic = new Sprite(GetPath("M72a1"));
            if (ammo == 0) graphic = new Sprite(GetPath("M72a0"));
            base.Update();
        }
    }
}