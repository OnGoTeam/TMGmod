using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{

    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MP5 : BaseBurst, IAmSmg
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
            center = new Vec2(13.5f, 6f);
            collisionOffset = new Vec2(-13.5f, -6f);
            collisionSize = new Vec2(27f, 12f);
            _barrelOffsetTL = new Vec2(27f, 3f);
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 1.3f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(-1f, 2f);
            _editorName = "MP5";
			weight = 3f;
            KforceDSmg = 2f;
            MaxAccuracy = 0.9f;
            MaxDelayFp = 10;
            MaxDelaySmg = 50;
            DeltaWait = 0.35f;
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
                        _fireWait = 1.3f;
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
