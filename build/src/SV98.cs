using DuckGame;


// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper")]
    // ReSharper disable once InconsistentNaming
    public class SV98 : Sniper
    {
        public SV98(float xval, float yval) : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("SV99"));
            _center = new Vec2(13.5f, 4.5f);
            _collisionOffset = new Vec2(-13.5f, -4.5f);
            _collisionSize = new Vec2(27f, 9f);
            _barrelOffsetTL = new Vec2(28f, 5f);
            ammo = 6;
            _ammoType = new AT9mm {penetration = 1f, range = 1000f};
            _fireSound = GetPath("sounds/Silenced3.wav");
            _fullAuto = false;
            _kickForce = 1.25f;
            loseAccuracy = 0.5f;
            maxAccuracyLost = 1.5f;
            _holdOffset = new Vec2(1f, 0f);
			_manualLoad = true;
            _editorName = "SV-99";
			_weight = 2f;
		}
        
        public override void Update()
		{
			base.Update();
			if (_loadState > -1)
            {
                if (owner == null)
				{
					if (_loadState == 2)
					{
						loaded = true;
					}
					_loadState = -1;
					_angleOffset = 0f;
					handOffset = Vec2.Zero;
				}

                switch (_loadState)
                {
                    case 0:
                    {
                        if (Network.isActive)
                        {
                            if (isServerForObject)
                            {
                                _netLoad.Play();
                            }
                        }
                        else
                        {
                            SFX.Play("loadSniper");
                        }
                        _loadState++;
                        break;
                    }

                    case 1 when _angleOffset < 0.16f:
                        _angleOffset = MathHelper.Lerp(_angleOffset, 0.25f, 0.25f);
                        break;
                    case 1:
                        _loadState++;
                        break;
                    case -1:
                    {
                        handOffset.x += 0.8f;
                        if (handOffset.x > 4f)
                        {
                            _loadState++;
                            Reload();
                            loaded = false;
                        }

                        break;
                    }

                    case 3:
                    {
                        handOffset.x -= 0.8f;
                        if (handOffset.x <= 0f)
                        {
                            _loadState++;
                            handOffset.x = 0f;
                        }

                        break;
                    }

                    case 4 when _angleOffset > 0.04f:
                        _angleOffset = MathHelper.Lerp(_angleOffset, 0f, 0.25f);
                        break;
                    case 4:
                        _loadState = -1;
                        loaded = true;
                        _angleOffset = 0f;
                        break;
                }
            }
			if (loaded && owner != null && _loadState == -1)
			{
				laserSight = false;
				return;
			}
			laserSight = false;
		}

		public override void OnPressAction()
		{
            _ammoType.accuracy = _owner.velocity != new Vec2(0f, 0f) ? 0f : 1f;
			if (loaded)
			{
				base.OnPressAction();
				return;
			}

            if (ammo <= 0 || _loadState != -1) return;
            _loadState = 0;
            _loadAnimation = 0;
        }

		public override void Draw()
		{
			var ang = angle;
			if (offDir > 0)
			{
				angle -= _angleOffset;
			}
			else
			{
				angle += _angleOffset;
			}
			base.Draw();
			angle = ang;
		}

        
	}
}