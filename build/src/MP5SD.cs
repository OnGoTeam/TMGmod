using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{

    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MP5SD : BaseBurst, IAmSmg, IFirstPrecise
    {
        public MP5SD(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mmS
            {
                range = 235f,
                accuracy = 0.8f
            };
            BaseAccuracy = 0.8f;
            _type = "gun";
            graphic = new Sprite(GetPath("MP5SDBurst"));
            center = new Vec2(15.5f, 6f);
            collisionOffset = new Vec2(-15.5f, -6f);
            collisionSize = new Vec2(31f, 12f);
            _barrelOffsetTL = new Vec2(31f, 3f);
            _fireSound = GetPath("sounds/Silenced2.wav");
            _fullAuto = false;
            _fireWait = 0.7f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(0f, 2f);
            _editorName = "MP5SD";
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
                        graphic = new Sprite(GetPath("MP5SDNonAuto"));
                    }
                    else
                    {
                        BurstNum = 3;
                        _fireWait = 1.3f;
                        graphic = new Sprite(GetPath("MP5SDBurst"));
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
