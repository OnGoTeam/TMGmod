using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Core.WClasses.ClassImplementations
{
    public abstract class BasePumpAction : BaseGun, IAmSg
    {
        private const sbyte LoadStart = -100;
        private const sbyte LoadNo = -1;
        private const sbyte LoadPop = 50;
        private const sbyte LoadFin = 100;
        protected float Loaddx = 3f;
        protected SpriteMap LoaderSprite;
        protected Vec2 LoaderVec2;

        [UsedImplicitly] public int LoadProgress;

        [UsedImplicitly] public StateBinding LoadProgressBinding = new(nameof(LoadProgress));

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
            LoadProgress = Math.Min(LoadProgress + LoadSpeed * (duck?._hovering == true ? 2 : 1), LoadFin);
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
            if (Level.current is Editor && this is IHaveAllowedSkins iha0 && !iha0.AllowedSkins.Contains(SkinValue)) return;
            var lp = LoadProgress;
            if (lp < 0) lp = 0;
            var num = (float)Math.Sin(lp * 0.031415) * Loaddx;
            var lc = LoaderSprite.center;
            if (Level.current is Editor && this is IHaveAllowedSkins iha && !iha.AllowedSkins.Contains(SkinValue))
                ContextSkinRender.WithRandomized(
                    new SkinMix(iha, LoaderSprite),
                    sprite =>
                    {
                        sprite.center = lc;
                        Draw(sprite, new Vec2(LoaderVec2.x - num, LoaderVec2.y));
                    }
                );
            else
                Draw(LoaderSprite, new Vec2(LoaderVec2.x - num, LoaderVec2.y));
        }

        private static readonly Dictionary<SpriteMap, SpriteMap> Rendered = new();

        private SpriteMap Render(Sprite smap)
        {
            var tbase = smap.texture;
            var tloader = LoaderSprite.texture;
            var tresult = new Tex2D(tbase.width, tbase.height);
            var dbase = tbase.GetData();
            var dloader = tloader.GetData();
            var dresult = new Color[tbase.width * tbase.height];
            var lp = center + LoaderVec2 - LoaderSprite.center;
            var lx = (int)Math.Round(lp.x);
            var ly = (int)Math.Round(lp.y);
            var lx1 = lx + LoaderSprite.width;
            var ly1 = ly + LoaderSprite.height;
            for (var ix = 0; ix < tbase.width * tbase.height; ix++)
            {
                dresult[ix] = dbase[ix];
                var block = smap.height * tbase.width;
                var row = ix / block;
                var withinrow = ix % block;
                var ty = withinrow / tbase.width;
                var withinty = withinrow % tbase.width;
                var column = withinty / smap.width;
                var tx = withinty % smap.width;
                var skin = (row * (tbase.width / smap.width) + column) % SkinFrames;
                if (lx > tx || tx >= lx1 || ly > ty || ty >= ly1) continue;
                // else
                var mx = tx - lx;
                var my = ty - ly;
                var mcolumn = skin % (tloader.width / LoaderSprite.width);
                var mrow = skin / (tloader.width / LoaderSprite.width);
                var withinmy = LoaderSprite.width * mcolumn + mx;
                var withinmrow = tloader.width * my + withinmy;
                var mblock = LoaderSprite.height * tloader.width;
                var mix = mblock * mrow + withinmrow;
                var color = dloader[mix];
                if (color.a == 255)
                    dresult[ix] = dloader[mix];
            }

            tresult.SetData(dresult);
            return new SpriteMap(tresult, smap.width, smap.height);
        }

        protected override SpriteMap EditorSpriteMap()
        {
            var smap = base.EditorSpriteMap();
            if (!Rendered.ContainsKey(smap))
                Rendered[smap] = Render(smap);
            return Rendered[smap];
        }
    }
}
