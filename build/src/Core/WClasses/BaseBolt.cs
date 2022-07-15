using System;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseBolt : BaseGun, ISpeedAccuracy, IAmSr
    {
        [UsedImplicitly] public float AngleOffset;
        [UsedImplicitly] public StateBinding AngleOffsetBinding = new StateBinding(nameof(AngleOffset));
        [UsedImplicitly] public int LoadState = -1;
        [UsedImplicitly] public StateBinding LoadStateBinding = new StateBinding(nameof(LoadState));
        [UsedImplicitly] public NetSoundEffect NetLoad;
        [UsedImplicitly] public StateBinding NetLoadBinding = new NetSoundBinding(nameof(NetLoad));

        protected BaseBolt(float xval, float yval, string netLoad = "loadSniper") : base(xval, yval)
        {
            NetLoad = new NetSoundEffect(netLoad);
            MaxAccuracy = 1f;
            MinAccuracy = 0f;
            SpeedAccuracyThreshold = 0f;
            SpeedAccuracyHorizontal = 1f;
            SpeedAccuracyVertical = 0f;
            _manualLoad = true;
            _fullAuto = false;
            laserSight = false;
        }

        public float SpeedAccuracyThreshold { get; }
        public float SpeedAccuracyHorizontal { get; }
        public float SpeedAccuracyVertical { get; }

        protected virtual bool HasLaser()
        {
            return false;
        }

        protected virtual float MaxAngle()
        {
            return 0.16f;
        }

        protected virtual float MaxOffset()
        {
            return 4f;
        }

        protected virtual float ReloadSpeed()
        {
            return 1f;
        }

        private float AngleSpeed()
        {
            return .15f * ReloadSpeed();
        }

        private float OffsetSpeed()
        {
            return .1f * MaxOffset() * ReloadSpeed();
        }

        private void PlayLoad()
        {
            if (Network.isActive)
            {
                if (isServerForObject)
                    NetLoad.Play();
            }
            else
                SFX.Play("loadSniper");
        }

        public override void Update()
        {
            base.Update();
            if (LoadState > -1)
            {
                if (owner == null)
                {
                    if (LoadState == 3)
                        loaded = true;
                    LoadState = -1;
                    AngleOffset = 0.0f;
                    handOffset = Vec2.Zero;
                }

                switch (LoadState)
                {
                    case 0:
                    {
                        PlayLoad();

                        ++LoadState;
                        break;
                    }
                    case 1 when AngleOffset < MaxAngle():
                        AngleOffset = MathHelper.Lerp(AngleOffset, 1.25f * MaxAngle(), AngleSpeed());
                        break;
                    case 1:
                        ++LoadState;
                        break;
                    case 2:
                    {
                        handOffset.x += OffsetSpeed();
                        if (handOffset.x * Math.Sign(MaxOffset()) > MaxOffset() * Math.Sign(MaxOffset()))
                        {
                            ++LoadState;
                            Reload();
                            loaded = false;
                        }

                        break;
                    }
                    case 3:
                    {
                        handOffset.x -= OffsetSpeed();
                        if (handOffset.x * Math.Sign(MaxOffset()) <= 0.0f)
                        {
                            ++LoadState;
                            handOffset.x = 0.0f;
                        }

                        break;
                    }
                    case 4 when AngleOffset > .25 * MaxAngle():
                        AngleOffset = MathHelper.Lerp(AngleOffset, 0.0f, AngleSpeed());
                        break;
                    case 4:
                        LoadState = -1;
                        loaded = true;
                        AngleOffset = 0.0f;
                        break;
                }
            }

            if (loaded && owner != null && LoadState == -1)
                laserSight = HasLaser();
            else
                laserSight = false;
        }

        public override void OnPressAction()
        {
            if (loaded)
                base.OnPressAction();
            else
            {
                if (ammo <= 0 || LoadState != -1)
                    return;
                LoadState = 0;
            }
        }

        public override void Draw()
        {
            var ang = angle;
            if (offDir > 0)
                angle -= AngleOffset;
            else
                angle += AngleOffset;
            base.Draw();
            angle = ang;
        }
    }
}
