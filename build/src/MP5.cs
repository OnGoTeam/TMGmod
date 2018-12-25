using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{

    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MP5 : BaseBurst, IAmSmg, IFirstPrecise
    {
        public MP5(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 215f,
                accuracy = 0.7f
            };
            BaseAccuracy = 0.7f;
            _type = "gun";
            graphic = new Sprite(GetPath("MP5Burst"));
            center = new Vec2(12f, 5f);
            collisionOffset = new Vec2(-11f, -4f);
            collisionSize = new Vec2(22f, 14f);
            _barrelOffsetTL = new Vec2(25f, 2f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(3f, 1f);
            _editorName = "MP40";
			weight = 3f;
            KforceDSmg = 2f;
            MaxAccuracy = 0.9f;
            MaxDelayFp = 10;
            MaxDelaySmg = 50;
            DeltaWait = 0.1f;
            BurstNum = 3;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
                    if (BurstNum == 3)
                    {
                        BurstNum = 1;
                        _fireWait = 0.3f;
                        graphic = new Sprite(GetPath("MP5NonAuto"));
                    }
                    else
                    {
                        BurstNum = 3;
                        _fireWait = 0.7f;
                        graphic = new Sprite(GetPath("MP5Burst"));
                    }
                }
            }
            base.Update();
        }
        public float KforceDSmg { get; }
        public int CurrDelaySmg { get; set; }
        public int CurrDelay { get; set; }
        public int MaxDelayFp { get; }
        public int MaxDelaySmg { get; set; }
        public float MaxAccuracy { get; }
    }
}
