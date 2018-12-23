using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorY : Holdable, IPlatform
    {
        private Type _contains;
        private const int CaseId = 4;

        public PodarokColorY (float xval, float yval)
          : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            graphic = sprite;
		    sprite.frame = 3;
		    center = new Vec2(7f, 4f);
            collisionOffset = new Vec2(-7f, -4f);
            collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Yellow Container";
        }


		public override void Initialize()
	{
		var things = new List<Type>
		{
            typeof(CZ75),
            typeof(AF2011),
            typeof(M93R),	
            typeof(MAP),	
            typeof(MPA27),	
            typeof(BigShot),	
            typeof(Nellegalja),
            typeof(Rfb),	
            typeof(Arx200),	
            typeof(SV98),	
            typeof(USP),	
            typeof(UziPro),	
            typeof(SIX12S),
            typeof(DaewooK1)
		};
		_contains = things[Rando.Int(things.Count - 1)];
	}

	public override void OnPressAction()
	{
	    if (owner == null) return;
        //else
	    var o = owner;
	    var d = duck;
	    if (d != null)
	    {
	        d.profile.stats.presentsOpened++;
	        duck.ThrowItem();
	    }
	    Level.Remove(this);
	    {
	        Initialize();
	    }
	    if (!(Editor.CreateThing(_contains) is Holdable newThing)) return;
	    if (Rando.Int(500) == 1 && newThing is Gun && (newThing as Gun).CanSpawnInfinite())
	    {
	        (newThing as Gun).infiniteAmmoVal = true;
	        (newThing as Gun).infinite.value = true;
	    }
	    newThing.x = o.x;
	    newThing.y = o.y;
	    if (newThing is IHaveSkin skinThing)
	    {
	        skinThing.FrameId = CaseId;
	    }
	    Level.Add(newThing);
	    if (d == null) return;
        //else
	    d.GiveHoldable(newThing);
	    d.resetAction = true;
	}
    }
}