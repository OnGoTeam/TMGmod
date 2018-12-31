using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Custom_Guns;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Cases
{
    [EditorGroup("TMG|Misc|Cases")]
    public class Hpodarok : Holdable, IPlatform
    {
        private Type _contains;

        public Hpodarok (float xval, float yval)
          : base(xval, yval)
        {
            graphic = new Sprite(GetPath("HeavyMillitaryCase"));
		    center = new Vec2(7f, 4f);
            collisionOffset = new Vec2(-7f, -4f);
            collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Heavy Millitary Container";
        }


		public override void Initialize()
	{
		var things = new List<Type>
		{
        typeof(AKALFA),
        typeof(BarretM98),
        typeof(BarretM98C),
        typeof(M16LMG),	
        typeof(M50),	
        typeof(MG44),	
        typeof(MG3),	
        typeof(PPSh),	
        typeof(PPShC),
        typeof(ScarGL),	
        typeof(SNR22),	
        typeof(SV98),	
        typeof(SVU),	
        typeof(SVUC),		
        typeof(TR21),	
        typeof(TR21C),	
        typeof(Vintorez),
        typeof(VintorezC),
        typeof(X3X)
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