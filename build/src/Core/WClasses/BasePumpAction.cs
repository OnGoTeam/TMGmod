using System;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.WClasses
{
    /// <inheritdoc cref="BaseGun"/>
    /// /// <inheritdoc cref="IAmSg"/>
    /// <summary>
    /// Base class (<see cref="BaseGun"/>) for pump-action Shotguns (<see cref="IAmSg"/>)
    /// </summary>
    public abstract class BasePumpAction:BaseGun, IAmSg
    {
        /// <summary>
        /// 
        /// </summary>
        [UsedImplicitly]
        public int LoadProgress;
        /// <summary>
        /// <see cref="LoadProgress"/> syncing
        /// </summary>
        [UsedImplicitly]
        public StateBinding LoadProgressBinding = new StateBinding(nameof(LoadProgress));
        private float _loadAnimation = 1f;
        /// <summary>
        /// Time when this reloads (<see cref="Gun.Reload"/>)
        /// </summary>
        protected sbyte EpsilonA = 50;
        /// <summary>
        /// Reload cycle length
        /// </summary>
        protected int EpsilonB = 100;
        /// <summary>
        /// Pump loader sprite
        /// </summary>
        protected SpriteMap LoaderSprite;
        /// <summary>
        /// Loader offset
        /// </summary>
        protected Vec2 LoaderVec2;
        /// <summary>
        /// Load delta x
        /// </summary>
        protected float Loaddx = 3f;

        /// <inheritdoc />
        protected BasePumpAction(float xval, float yval) : base(xval, yval)
        {
            LoadProgress = EpsilonB;
        }

        /// <inheritdoc />
        /// <summary>
        /// Load animation update
        /// </summary>
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

        /// <inheritdoc />
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

        /// <summary>
        /// Drawing loader
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            var num = (float)Math.Sin(_loadAnimation * 3.1415) * Loaddx;
            Draw(LoaderSprite, new Vec2(LoaderVec2.x - num, LoaderVec2.y));
        }
    }
}