﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
#if DEBUG
using TMGmod.Useless_or_deleted_Guns;
#endif

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    [UsedImplicitly]
    public class PodarokClassic : BaseCase
    {
        private int _frames;

        public PodarokClassic(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseClassic"), 14, 8);
            _graphic = sprite;
            sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Classic Case";
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                B<NellegaljaMk2>().Skin(BaseColor.Green),
                B<Alep30>().Skin(BaseColor.Alt),
                B<BigShot>().Skin(BaseColor.Red),
                B<AF2011>(),
                B<AKALFA>().Skin(BaseColor.Fifth),
                B<AN94>(),
                B<BarretM98>(),
                B<X3X>().Skin(BaseColor.Random),
                B<SVU>(),
                B<Deadly44>(),
                B<MP40>(),
#if DEBUG
                B<PPSh41>().Chance(.5f),
                B<PPSh>().Chance(.5f),
#else
                B<PPSh41>(),
#endif
            };
            CaseColor = (int)BaseColor.No;
        }

        public override void Update()
        {
            ++_frames;
            base.Update();
        }

        public override void Draw()
        {
            ((SpriteMap)_graphic).frame = (Level.activeLevel is Editor ? MonoMain.timeInEditor : _frames) / 6 % 4;
            base.Draw();
        }
    }
}
