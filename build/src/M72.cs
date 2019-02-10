using DuckGame;
using JetBrains.Annotations;

namespace TMGmod
{
    [EditorGroup("TMG|Misc|Grenadelauncher")]
    [PublicAPI]
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
                barrelAngleDegrees = -7.5f,
                bulletSpeed = 10f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("M72a5"));
            _center = new Vec2(16f, 5.5f);
            _collisionOffset = new Vec2(-16f, -5.5f);
            _collisionSize = new Vec2(32f, 11f);
            _barrelOffsetTL = new Vec2(32f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 1f;
            _kickForce = 1.2f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            _editorName = "M72 Grenade Launcher";
            _weight = 4.5f;
        }
        public override void Update()
        {
            switch (ammo)
            {
                case 4:
                    graphic = new Sprite(GetPath("M72a4"));
                    break;
                case 3:
                    graphic = new Sprite(GetPath("M72a3"));
                    break;
                case 2:
                    graphic = new Sprite(GetPath("M72a2"));
                    break;
                case 1:
                    graphic = new Sprite(GetPath("M72a1"));
                    break;
                case 0:
                    graphic = new Sprite(GetPath("M72a0"));
                    break;
            }

            base.Update();
        }
    }
}