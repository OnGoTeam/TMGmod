using System;
using System.Collections.Generic;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Cases")]
    public class mpodarok : Holdable, IPlatform
    {
        private System.Type _contains;

        public mpodarok (float xval, float yval)
          : base(xval, yval)
        {
            this.graphic = new Sprite(GetPath("MillitaryCase"));
		    this.center = new Vec2(7f, 4f);
            this.collisionOffset = new Vec2(-7f, -4f);
            this.collisionSize = new Vec2(14f, 8f);
            this.depth = -0.5f;
            this.thickness = 0.0f;
            this.weight = 3f;
            this.collideSounds.Add("presentLand");
            this._editorName = "Millitary Container";
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
		this._contains = things[Rando.Int(things.Count - 1)];
	}

	public override void OnPressAction()
	{
		
		if (this.owner != null)
		{
			Thing o = this.owner;
			Duck d = base.duck;
			if (d != null)
			{
				d.profile.stats.presentsOpened++;
				base.duck.ThrowItem(true);
			}
			Level.Remove(this);
			{
				this.Initialize();
			}
			Holdable newThing = Editor.CreateThing(this._contains) as Holdable;
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