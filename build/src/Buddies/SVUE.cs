#if DEBUG
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|Sniper")]
    // ReSharper disable once InconsistentNaming
    public class SVUE: BaseGun, IAmDmr, MagBuddy.ISupportReload
    {
        private readonly MagBuddy _magBuddy;

        public SVUE (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 10;
            _ammoType = new ATMagnum
            {
                range = 580f,
                accuracy = 0.91f,
                penetration = 1.5f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("SVU"));
            _center = new Vec2(20f, 8f);
            _collisionOffset = new Vec2(-14.5f, -8f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 5f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 1.5f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.45f;
            _holdOffset = new Vec2(1f, 2f);
            _editorName = "SVUE";
			_weight = 5f;
            _magBuddy = new MagBuddy(this, typeof(UziPro));
        }

        public override void OnPressAction()
        {
            base.OnPressAction();
            if (ammo <= 0) _magBuddy.Reload();
        }

        public bool SetMag()
        {
            ammo = 10;
            return true;
        }

        public bool DropMag(Thing mag)
        {
            SFX.Play("magnum");
            return true;
        }

        public Vec2 SpawnPos { get; set; }
    }
}
#endif