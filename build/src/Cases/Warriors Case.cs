using System;
using System.Collections.Generic;
using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Cases
{
    [EditorGroup("TMG|Misc|Cases")]
    public class Warriorscase : Holdable, IPlatform
    {
        private Type _contains;

        public Warriorscase (float xval, float yval)
          : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("WarriorsCase"), 14, 8);
            graphic = sprite;	
		    sprite.frame = Rando.Int(0, 4);
		    center = new Vec2(7f, 4f);
            collisionOffset = new Vec2(-7f, -4f);
            collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Warriors Case";
        }


		public override void Initialize()
	{
		var things = new List<Type>()
	    {
        typeof(USP),
        typeof(M93R),	
        typeof(M4A1),	
        typeof(SV98),
        typeof(USP),
        typeof(M93R),	
        typeof(M4A1),	
        typeof(SV98),
        typeof(USP),
        typeof(M93R),	
        typeof(M4A1),	
        typeof(SV98),
        typeof(USP),
        typeof(M93R),	
        typeof(M4A1),	
        typeof(SV98),	
        typeof(Vintorez),	
        typeof(Ussrgun),
        typeof(MG44),	
        typeof(Vintorez),	
        typeof(Ussrgun),
        typeof(MG44),	
        typeof(Vintorez),	
        typeof(Ussrgun),
        typeof(MG44),	
        typeof(AN94),	
        typeof(CZ805),	
        typeof(AN94),	
        typeof(CZ805),	
        typeof(PPSh),	
        typeof(SVU)
		};
		_contains = things[Rando.Int(things.Count - 1)];
	}

	public override void OnPressAction()
	{
	    if (owner == null) return;
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
	    Level.Add(newThing);
	    if (d == null) return;
	    d.GiveHoldable(newThing);
	    d.resetAction = true;
	}
    }
}