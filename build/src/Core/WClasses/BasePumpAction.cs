using System;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    public abstract class BasePumpAction:BaseGun, IAmSg
    {
        [UsedImplicitly]
        public int LoadProgress;
        [UsedImplicitly]
        public StateBinding LoadProgressBinding = new StateBinding(nameof(LoadProgress));
        protected SpriteMap LoaderSprite;
        protected Vec2 LoaderVec2;
        protected float Loaddx = 3f;
        private const sbyte LoadStart = -100;
        private const sbyte LoadNo = -1;
        private const sbyte LoadPop = 50;
        private const sbyte LoadFin = 100;
        protected sbyte LoadSpeed = 10;
        protected BasePumpAction(float xval, float yval) : base(xval, yval)
        {
            LoadProgress = LoadFin;
        }

        private void UpdateAtPopReload()
        {
            if (LoadProgress < LoadPop) return;
            if (loaded) return;
            Reload();
        }

        private void UpdateLoad()
        {
            if (LoadProgress == LoadStart)
            {
                SFX.Play("shotgunLoad");
                LoadProgress = 0;
            }

            if (LoadProgress < 0) return;
            UpdateAtPopReload();
            LoadProgress = Math.Min(LoadProgress + LoadSpeed, LoadFin);
        }

        public override void Update()
        {
            base.Update();
            UpdateLoad();
        }

        public override void OnPressAction()
        {
            if (LoadProgress == LoadNo)
                LoadProgress = LoadStart;
            if (!loaded) return;
            base.OnPressAction();
            LoadProgress = LoadNo;
        }

        public override void Draw()
        {
            base.Draw();
            var num = (float)Math.Sin(LoadProgress * 0.031415) * Loaddx;
            Draw(LoaderSprite, new Vec2(LoaderVec2.x - num, LoaderVec2.y));
        }
    }
}