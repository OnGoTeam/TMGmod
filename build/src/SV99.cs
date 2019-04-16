using JetBrains.Annotations;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.WClasses;
using TMGmod.Core;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class SV99 : Sniper, IAmSr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 3 });
        public SV99(float xval, float yval) : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("SV99pattern"), 27, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(13.5f, 4.5f);
            _collisionOffset = new Vec2(-13.5f, -4.5f);
            _collisionSize = new Vec2(27f, 9f);
            _barrelOffsetTL = new Vec2(28f, 5f);
            ammo = 6;
            _ammoType = new AT9mm
            {
                penetration = 1f,
                range = 1000f
            };
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
                        if (!Network.isActive)
                        {
                            SFX.Play("loadSniper");
                        }
                        else if (isServerForObject)
                        {
                            _netLoad.Play();
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
			if (loaded && duck != null && _loadState == -1)
			{
				laserSight = false;
				return;
			}
			laserSight = false;
		}

		public override void OnPressAction()
		{
            _ammoType.accuracy = _owner == null || _owner.velocity != new Vec2(0f, 0f) ? 0f : 1f;
			if (loaded)
			{
				base.OnPressAction();
				return;
			}

		    if (ammo <= 0 || _loadState != -1) return;
            //else
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
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}