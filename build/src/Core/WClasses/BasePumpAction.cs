using System;
using DuckGame;

namespace TMGmod.Core.WClasses
{
    public class BasePumpAction:BaseGun, IAmSg
    {
        public sbyte _loadProgress = 100;
        public float _loadAnimation = 1f;
        public StateBinding _loadProgressBinding = new StateBinding(nameof(_loadProgress), -1, false, false);
        protected SpriteMap _loaderSprite;

        public BasePumpAction(float xval, float yval) : base(xval, yval)
        {
        }

        public override void Update()
        {
            base.Update();
            if (_loadAnimation == -1.0)
            {
                SFX.Play("shotgunLoad", 1f, 0.0f, 0.0f, false);
                _loadAnimation = 0.0f;
            }
            if (_loadAnimation >= 0.0)
            {
                if (_loadAnimation == 0.5 && ammo != 0)
                    _ammoType.PopShell(x, y, -offDir);
                if (_loadAnimation < 1.0)
                    _loadAnimation += 0.1f;
                else
                    _loadAnimation = 1f;
            }
            if (_loadProgress < 0)
                return;
            if (_loadProgress == 50)
                Reload(false);
            if (_loadProgress < 100)
                _loadProgress += 10;
            else
                _loadProgress = 100;
        }

        public override void OnPressAction()
        {
            if (loaded)
            {
                base.OnPressAction();
                _loadProgress = -1;
                _loadAnimation = -0.01f;
            }
            else
            {
                if (_loadProgress != -1)
                    return;
                _loadProgress = 0;
                _loadAnimation = -1f;
            }
        }

        public override void Draw()
        {
            base.Draw();
            Vec2 vec2 = new Vec2(13f, -2f);
            float num = (float)Math.Sin(_loadAnimation * 3.14000010490417) * 3f;
            Draw(_loaderSprite, new Vec2(vec2.x - 8f - num, vec2.y + 4f), 1);
        }
    }
}