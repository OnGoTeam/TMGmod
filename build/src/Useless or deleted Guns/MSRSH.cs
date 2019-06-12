#if DEBUG
using System;
using DuckGame;
using JetBrains.Annotations;

// ReSharper disable VirtualMemberCallInConstructor


namespace TMGmod.Useless_or_deleted_Guns
{
    [EditorGroup("TMG|DEBUG")]
    [BaggedProperty("isInDemo", true), BaggedProperty("canSpawn", false)]
    [PublicAPI]
    [Obsolete]
    // ReSharper disable once InconsistentNaming
    public class MSRC : Sniper
    {
        private int _ammo2;
        private AmmoType _ammoType2;
        private Sprite _graphic2;
        private Vec2 _barrelOffsetTl2;
        private string _fireSound2;
        private float _loseAccuracy2;
        private float _maxAccuracyLost2;
        private int _numBulletsPerFire2;
        private bool _switched;
        private bool _fullAuto2;
        private bool _drobovik;
        private bool _snuper = true;
        //Are you see that? This is a sniper rifle with underbarrel shotgun. But it does not work. R.I.P.
        //Wait.. what are you doing here?!	
        public MSRC(float xval, float yval) : base(xval, yval)
        {
            graphic = new Sprite(GetPath("MSRSH"));
            _graphic2 = new Sprite(GetPath("MSRSH2"));
            center = new Vec2(28.5f, 6f);
            collisionOffset = new Vec2(-28.5f, -6f);
            collisionSize = new Vec2(47f, 12f);
            _barrelOffsetTL = new Vec2(48f, 5f);
            _barrelOffsetTl2 = new Vec2(14f, 6f);
            ammo = 5;
            _ammo2 = 4;
            _ammoType = new ATSniper
            {
                bulletSpeed = 85f
            };
            _ammoType2 = new AT9mm
            {
                range = 130f,
                accuracy = 1f,
                penetration = 1f,
                bulletSpeed = 40f
            };
            _numBulletsPerFire = 1;
            _numBulletsPerFire2 = 7;
            _fireSound = "sniper";
            _fireSound2 = "littleGun";
            _fullAuto = false;
            _fullAuto2 = false;
            _kickForce = 2f;
            _fireWait = 2f;
            laserSight = false;
            loseAccuracy = 0f;
            _loseAccuracy2 = 0f;
            maxAccuracyLost = 0f;
            _maxAccuracyLost2 = 0f;
            _laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(14f, -1f);
            _editorName = "MSR With SH";
            weight = 5.45f;
			

        }

        public override void Draw()
        {
            var ang = angle;
            if (offDir <= 0)
            {
                angle = angle + _angleOffset;
            }
            else
            {
                angle = angle - _angleOffset;
            }
            base.Draw();
            angle = ang;
            laserSight = false;
        }

        public override void OnPressAction()
        {
            if (loaded)
            {
                base.OnPressAction();
                return;
            }
            if (ammo > 0 && _loadState == -1 && _drobovik == false && _snuper)
            {
                _loadState = 0;
                _loadAnimation = 0;
            }
        }

        public override void Update()
        {
            base.Update();
            if (_loadState > -1 && _drobovik == false && _snuper)
            {
                if (owner == null)
                {
                    if (_loadState == 3)
                    {
                        loaded = true;
                    }
                    _loadState = -1;
                    _angleOffset = 0f;
                    handOffset = Vec2.Zero;
                }
                if (_loadState == 0)
                {
                    if (!Network.isActive)
                    {
                        SFX.Play("loadSniper");
                    }
                    else if (isServerForObject)
                    {
                        _netLoad.Play();
                    }
                    Sniper sniper = this;
                    sniper._loadState = sniper._loadState + 1;
                }
                else if (_loadState == 1)
                {
                    if (_angleOffset >= 0.1f)
                    {
                        Sniper sniper1 = this;
                        sniper1._loadState = sniper1._loadState + 1;
                    }
                    else
                    {
                        _angleOffset = _angleOffset + 0.003f;
                    }
                }
                else if (_loadState == 2)
                {
                    handOffset.x = handOffset.x - 0.2f;
                    if (handOffset.x > 4f)
                    {
                        Sniper sniper2 = this;
                        sniper2._loadState = sniper2._loadState + 1;
                        Reload();
                        loaded = false;
                    }
                }
                else if (_loadState == 3)
                {
                    handOffset.x = handOffset.x + 0.2f;
                    if (handOffset.x <= 0f)
                    {
                        Sniper sniper3 = this;
                        sniper3._loadState = sniper3._loadState + 1;
                        handOffset.x = 0f;
                    }
                }
                else if (_loadState == 4)
                {
                    if (_angleOffset <= 0.03f)
                    {
                        _loadState = -1;
                        loaded = true;
                        _angleOffset = 0f;
                    }
                    else
                    {
                        _angleOffset = MathHelper.Lerp(_angleOffset, 0f, 0.15f);
                    }
                }
            }
            laserSight = false;
            if (duck != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
                    if (!_switched)
                    {
                        _switched = true;
                        graphic = new Sprite(GetPath("MSRSH1"));
                    }
                    var g2 = _graphic2;
                    _graphic2 = graphic;
                    graphic = g2;
                    var la2 = _loseAccuracy2;
                    _loseAccuracy2 = loseAccuracy;
                    loseAccuracy = la2;
                    var mal2 = _maxAccuracyLost2;
                    _maxAccuracyLost2 = maxAccuracyLost;
                    maxAccuracyLost = mal2;
                    var fak = _numBulletsPerFire2;
                    _numBulletsPerFire2 = _numBulletsPerFire;
                    _numBulletsPerFire = fak;
                    var botl2 = _barrelOffsetTl2;
                    _barrelOffsetTl2 = _barrelOffsetTL;
                    _barrelOffsetTL = botl2;
                    var a2 = _ammo2;
                    _ammo2 = ammo;
                    ammo = a2;
                    var at2 = _ammoType2;
                    _ammoType2 = _ammoType;
                    _ammoType = at2;
                    var s2 = _fireSound2;
                    _fireSound2 = _fireSound;
                    _fireSound = s2;
                    var autonik = _fullAuto2;
                    _fullAuto2 = _fullAuto;
                    _fullAuto = autonik;
                    var stupidshit = _snuper;
                    _snuper = _drobovik;
                    _drobovik = stupidshit;
                }
            }
            base.Update();
        }
        public override void Thrown()
        {
            if (ammo == 0)
            {
                var g2 = _graphic2;
                _graphic2 = graphic;
                graphic = g2;
                var la2 = _loseAccuracy2;
                _loseAccuracy2 = loseAccuracy;
                loseAccuracy = la2;
                var mal2 = _maxAccuracyLost2;
                _maxAccuracyLost2 = maxAccuracyLost;
                maxAccuracyLost = mal2;
                var fak = _numBulletsPerFire2;
                _numBulletsPerFire2 = _numBulletsPerFire;
                _numBulletsPerFire = fak;
                var botl2 = _barrelOffsetTl2;
                _barrelOffsetTl2 = _barrelOffsetTL;
                _barrelOffsetTL = botl2;
                var a2 = _ammo2;
                _ammo2 = ammo;
                ammo = a2;
                var at2 = _ammoType2;
                _ammoType2 = _ammoType;
                _ammoType = at2;
                var s2 = _fireSound2;
                _fireSound2 = _fireSound;
                _fireSound = s2;
                var autonik = _fullAuto2;
                _fullAuto2 = _fullAuto;
                _fullAuto = autonik;
                var stupidshit = _snuper;
                _snuper = _drobovik;
                _drobovik = stupidshit;
            }
            base.Thrown();
        }		
    }
}
#endif