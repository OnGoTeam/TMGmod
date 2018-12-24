using System;
using DuckGame;

namespace TMGmod.Core.WClasses
{
    public abstract class BasePumpAction:BaseGun, IAmSg
    {
        protected sbyte LoadProgress;
        protected float LoadAnimation = 1f;
        protected sbyte EpsilonA;
        protected sbyte EpsilonB;
        protected SpriteMap LoaderSprite;
        protected Vec2 LoaderVec2;

        protected BasePumpAction(float xval, float yval) : base(xval, yval)
        {
            LoadProgress = EpsilonB;
        }

        public override void Update()
        {
            base.Update();
            if (Math.Abs(LoadAnimation - (-1.0)) < 0.02f)
            {
                SFX.Play("shotgunLoad");
                LoadAnimation = 0.0f;
            }
            if (LoadAnimation >= 0.0)
            {
                if (Math.Abs(LoadAnimation - 0.5) < 0.02f && ammo != 0)
                    _ammoType.PopShell(x, y, -offDir);
                if (LoadAnimation < 1.0)
                    LoadAnimation += 0.1f;
                else
                    LoadAnimation = 1f;
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
                LoadAnimation = -0.01f;
            }
            else
            {
                if (LoadProgress != -1)
                    return;
                LoadProgress = 0;
                LoadAnimation = -1f;
            }
        }

        public override void Draw()
        {
            base.Draw();
            var num = (float)Math.Sin(LoadAnimation * 3.14000010490417) * 3f;
            Draw(LoaderSprite, new Vec2(LoaderVec2.x - num, LoaderVec2.y));
        }
    }
}