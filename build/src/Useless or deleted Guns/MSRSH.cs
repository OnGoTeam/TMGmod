using DuckGame;


namespace TMGmod.src
{
    [BaggedProperty("isInDemo", true), BaggedProperty("canSpawn", false)]
    public class MSRC : Sniper
    {
        int ammo2;
        AmmoType _ammoType2;
        Sprite graphic1;
        Sprite graphic2;
        Vec2 _barrelOffsetTL2;
		string _fireSound2;
        float loseAccuracy2;
        float maxAccuracyLost2;
        int _numBulletsPerFire2;
		bool switched = false;
		bool _fullAuto2 = false;
		bool drobovik = false;
		bool snuper = true;
	//Are you see that? This is a sniper rifle with underbarrel shotgun. But it does not work. R.I.P.
    //Wait.. what are you doing here?!	
        public MSRC(float xval, float yval) : base(xval, yval)
        {
            graphic = new Sprite(GetPath("MSRSH"));
            graphic1 = new Sprite(GetPath("MSRSH1"));
            graphic2 = new Sprite(GetPath("MSRSH2"));
            center = new Vec2(28.5f, 6f);
            collisionOffset = new Vec2(-28.5f, -6f);
            collisionSize = new Vec2(47f, 12f);
            _barrelOffsetTL = new Vec2(48f, 5f);
            _barrelOffsetTL2 = new Vec2(14f, 6f);
            ammo = 5;
			ammo2 = 4;
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
            loseAccuracy2 = 0f;
            maxAccuracyLost = 0f;
            maxAccuracyLost2 = 0f;
            _laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(14f, -1f);
            _editorName = "MSR With SH";
			weight = 5.45f;
			

        }

        public override void Draw()
        {
            float ang = angle;
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
            if (ammo > 0 && _loadState == -1 && drobovik == false && snuper == true)
            {
                _loadState = 0;
                _loadAnimation = 0;
            }
        }

        public override void Update()
        {
            base.Update();
            if (_loadState > -1 && drobovik == false && snuper == true)
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
                        SFX.Play("loadSniper", 1f, 0f, 0f, false);
                    }
                    else if (isServerForObject)
                    {
                        _netLoad.Play(1f, 0f);
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
                        Reload(true);
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
			            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK", false))
                    {
						if (!switched)
						{
							switched = true;
                            graphic = new Sprite(GetPath("MSRSH1"));
                        }
    			        Sprite g2 = graphic2;
                        graphic2 = graphic;
                        graphic = g2;
                        float la2 = loseAccuracy2;
                        loseAccuracy2 = loseAccuracy;
                        loseAccuracy = la2;
    			        float mal2 = maxAccuracyLost2;
                        maxAccuracyLost2 = maxAccuracyLost;
                        maxAccuracyLost = mal2;
    			        int fak = _numBulletsPerFire2;
                        _numBulletsPerFire2 = _numBulletsPerFire;
                        _numBulletsPerFire = fak;
                        Vec2 botl2 = _barrelOffsetTL2;
                        _barrelOffsetTL2 = _barrelOffsetTL;
                        _barrelOffsetTL = botl2;
                        int a2 = ammo2;
                        ammo2 = ammo;
                        ammo = a2;
                        AmmoType at2 = _ammoType2;
                        _ammoType2 = _ammoType;
                        _ammoType = at2;
						string s2 = _fireSound2;
						_fireSound2 = _fireSound;
						_fireSound = s2;
                        bool autonik = _fullAuto2;
                        _fullAuto2 = _fullAuto;
                        _fullAuto = autonik;
                        bool stupidshit = snuper;
                        snuper = drobovik;
                        drobovik = stupidshit;
					}
				}
			}
		    base.Update();
		}
        public override void Thrown()
        {
            if (ammo == 0)
            {
                Sprite g2 = graphic2;
                graphic2 = graphic;
                graphic = g2;
                float la2 = loseAccuracy2;
                loseAccuracy2 = loseAccuracy;
                loseAccuracy = la2;
                float mal2 = maxAccuracyLost2;
                maxAccuracyLost2 = maxAccuracyLost;
                maxAccuracyLost = mal2;
    			int fak = _numBulletsPerFire2;
                _numBulletsPerFire2 = _numBulletsPerFire;
                _numBulletsPerFire = fak;
                Vec2 botl2 = _barrelOffsetTL2;
                _barrelOffsetTL2 = _barrelOffsetTL;
                _barrelOffsetTL = botl2;
                int a2 = ammo2;
                ammo2 = ammo;
                ammo = a2;
                AmmoType at2 = _ammoType2;
                _ammoType2 = _ammoType;
                _ammoType = at2;
				string s2 = _fireSound2;
				_fireSound2 = _fireSound;
				_fireSound = s2;
                bool autonik = _fullAuto2;
                _fullAuto2 = _fullAuto;
                _fullAuto = autonik;
                bool stupidshit = snuper;
                snuper = drobovik;
                drobovik = stupidshit;
            }
            base.Thrown();
        }		
        }
	}