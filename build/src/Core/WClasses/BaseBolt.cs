#if DEBUG
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public abstract class BaseBolt : BaseGun, ISpeedAccuracy, IAmSr
    {
        [UsedImplicitly] public StateBinding LoadStateBinding = new StateBinding(nameof(LoadState));
        [UsedImplicitly] public StateBinding AngleOffsetBinding = new StateBinding(nameof(AngleOffset));
        [UsedImplicitly] public StateBinding NetLoadBinding = new NetSoundBinding(nameof(NetLoad));
        [UsedImplicitly] public NetSoundEffect NetLoad;
        [UsedImplicitly] public int LoadState = -1;
        [UsedImplicitly] public float AngleOffset;
        protected virtual bool HasLaser() => false;

        protected BaseBolt(float xval, float yval, string netLoad="loadSniper") : base(xval, yval)
        {
            NetLoad = new NetSoundEffect(netLoad);
            BaseAccuracy = 1f;
            MinAccuracy = 0f;
            SpeedAccuracyThreshold = 0f;
            SpeedAccuracyHorizontal = 1f;
            SpeedAccuracyVertical = 0f;
            _manualLoad = true;
        }

        public float SpeedAccuracyThreshold { get; }
        public float SpeedAccuracyHorizontal { get; }
        public float SpeedAccuracyVertical { get; }

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
                        if (Network.isActive)
                        {
                            if (isServerForObject)
                                NetLoad.Play();
                        }
                        else
                            SFX.Play("loadSniper");

                        ++LoadState;
                        break;
                    }
                    case 1 when AngleOffset < 0.16f:
                        AngleOffset = MathHelper.Lerp(AngleOffset, 0.2f, 0.15f);
                        break;
                    case 1:
                        ++LoadState;
                        break;
                    case 2:
                    {
                        handOffset.x += 0.4f;
                        if (handOffset.x > 4.0f)
                        {
                            ++LoadState;
                            Reload();
                            loaded = false;
                        }

                        break;
                    }
                    case 3:
                    {
                        handOffset.x -= 0.4f;
                        if (handOffset.x <= 0.0f)
                        {
                            ++LoadState;
                            handOffset.x = 0.0f;
                        }

                        break;
                    }
                    case 4 when AngleOffset > 0.04f:
                        AngleOffset = MathHelper.Lerp(AngleOffset, 0.0f, 0.15f);
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
#endif
