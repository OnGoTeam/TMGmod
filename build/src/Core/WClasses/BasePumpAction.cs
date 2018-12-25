using System;
using DuckGame;

namespace TMGmod.Core.WClasses
{
    public abstract class BasePumpAction:BaseGun, IAmSg
    {
        public int LoadProgress;
        public StateBinding LoadProgressBinding = new StateBinding(nameof(LoadProgress));
        private float _loadAnimation = 1f;
        protected sbyte EpsilonA = 50;
        protected int EpsilonB = 100;
        protected SpriteMap LoaderSprite;
        protected Vec2 LoaderVec2;
        protected float Loaddx = 3f;

        protected BasePumpAction(float xval, float yval) : base(xval, yval)
        {
            LoadProgress = EpsilonB;
        }

        public override void Update()
        {
            base.Update();
            if (Math.Abs(_loadAnimation - -1.0) < 0.02f)
            {
                SFX.Play("shotgunLoad");
                _loadAnimation = 0.0f;
            }
            if (_loadAnimation >= 0.0)
            {
                if (Math.Abs(_loadAnimation - 0.5) < 0.02f && ammo != 0)
                    _ammoType.PopShell(x, y, -offDir);
                if (_loadAnimation < 1.0)
                    _loadAnimation += 0.1f;
                else
                    _loadAnimation = 1f;
            }
            if (LoadProgress < 0)
                return;
            if (LoadProgress == EpsilonA)
                Reload(false);
            if (LoadProgress < EpsilonB)
                LoadProgress += 10;
            else
                LoadProgress = EpsilonB;
        }

        public override void OnPressAction()
        {
            if (loaded)
            {
                base.OnPressAction();
                LoadProgress = -1;
                _loadAnimation = -0.01f;
            }
            else
            {
                if (LoadProgress != -1)
                    return;
                LoadProgress = 0;
                _loadAnimation = -1f;
            }
        }

        public override void Draw()
        {
            base.Draw();
            var num = (float)Math.Sin(_loadAnimation * 3.1415) * Loaddx;
            Draw(LoaderSprite, new Vec2(LoaderVec2.x - num, LoaderVec2.y));
        }
    }
}