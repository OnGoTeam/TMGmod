using System;
using System.Collections.Generic;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Cases")]
    public class mpodarok : Holdable, IPlatform
    {
        private Type _contains;

        public mpodarok (float xval, float yval)
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
		List<Type> things = new List<Type>()
	    {
        typeof(AN94),
        typeof(AN94C),
        typeof(aug),
        typeof(cz805),	
        typeof(DragoShot),	
        typeof(Glock18),
        typeof(Glock18C),	
        typeof(HazeS),	
        typeof(M4A1),
        typeof(M960),
        typeof(MG44C),	
        typeof(MP40),	
        typeof(P90),	
        typeof(ussrgun),	
        typeof(scarpdw),	
        typeof(SIX12),	
        typeof(SIX12C),	
        typeof(Vag),	
		};
		_contains = things[Rando.Int(things.Count - 1)];
	}

	public override void OnPressAction()
	{
		
		if (owner != null)
		{
			Thing o = owner;
			Duck d = duck;
			if (d != null)
			{
				d.profile.stats.presentsOpened++;
				duck.ThrowItem(true);
			}
			Level.Remove(this);
			{
				Initialize();
			}
			Holdable newThing = Editor.CreateThing(_contains) as Holdable;
			if (newThing != null)
			{
				if (Rando.Int(500) == 1 && newThing is Gun && (newThing as Gun).CanSpawnInfinite())
				{
					(newThing as Gun).infiniteAmmoVal = true;
					(newThing as Gun).infinite.value = true;
				}
				newThing.x = o.x;
				newThing.y = o.y;
				Level.Add(newThing);
				if (d != null)
				{
					d.GiveHoldable(newThing);
					d.resetAction = true;
				}
			}
		}
	}
    }
}