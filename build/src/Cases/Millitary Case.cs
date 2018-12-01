using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Custom_Guns;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Cases
{
    [EditorGroup("TMG|Misc|Cases")]
    public class Mpodarok : Holdable, IPlatform
    {
        private Type _contains;

        public Mpodarok (float xval, float yval)
          : base(xval, yval)
        {
            graphic = new Sprite(GetPath("MillitaryCase"));
		    center = new Vec2(7f, 4f);
            collisionOffset = new Vec2(-7f, -4f);
            collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Millitary Container";
        }


		public override void Initialize()
	{
		var things = new List<Type>()
	    {
        typeof(AN94),
        typeof(AN94C),
        typeof(Aug),
        typeof(CZ805),	
        typeof(DragoShot),	
        typeof(Glock18),
        typeof(Glock18C),	
        typeof(HazeS),	
        typeof(M4A1),
        typeof(M960),
        typeof(MG44C),	
        typeof(MP40),	
        typeof(P90),	
        typeof(Ussrgun),	
        typeof(Scarpdw),	
        typeof(SIX12),	
        typeof(SIX12C),	
        typeof(Vag),	
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