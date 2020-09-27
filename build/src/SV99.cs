using JetBrains.Annotations;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.WClasses;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class SV99 : Sniper, IAmSr, IHaveSkin, I5
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 3, 5, 8 });
        public SV99(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(8, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("SV99"), 27, 9);
            _graphic = _sprite;
            _sprite.frame = 8;
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(27f, 9f);
            _barrelOffsetTL = new Vec2(27f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            ammo = 6;
            _ammoType = new ATSV99();
            _fireSound = GetPath("sounds/Silenced3.wav");
            _fullAuto = false;
            _kickForce = 1.8f;
            loseAccuracy = 0.5f;
            maxAccuracyLost = 1.5f;
            _holdOffset = new Vec2(-1f, 0f);
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

                // ReSharper disable once SwitchStatementMissingSomeCases
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
        [UsedImplicitly]
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