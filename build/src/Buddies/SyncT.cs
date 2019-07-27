using DuckGame;
using JetBrains.Annotations;
using OgtDgLib.Ext;

namespace TMGmod.Buddies
{
    [PublicAPI]
    [EditorGroup("TMG|DEBUG")]
    public class SyncT:SyncedGun
    {
        private readonly byte[] _ats;
        public SyncT(float xval, float yval) : base(xval, yval, new SpriteMap(Mod.GetPath<Core.TMGmod>("PPSH41"), 30, 8), 1)
        {
            ammo = 71;
            _ammoType = new AT9mm
            {
                range = 200f,
                accuracy = 0.73f,
                penetration = 0.4f
            };
            _ats = AmmoTypeSerialized.GetBytes();
            _type = "gun";
            _center = new Vec2(15f, 4f);
            _collisionOffset = new Vec2(-15f, -4f);
            _collisionSize = new Vec2(30f, 8f);
            _barrelOffsetTL = new Vec2(29f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.25f;
            _kickForce = 1.2f;
            _holdOffset = new Vec2(2f, 1f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.4f;
            _editorName = "SCT";
            _weight = 3.5f;
        }

        public override void Update()
        {
            if (duck != null && duck.IsQuacking())
            {
                _ammoType.penetration = 100;
                _ammoType.range = 10000;
            }
            else
            {
                AmmoTypeSerialized = new BitBuffer(_ats);
            }
            base.Update();
        }
    }
}