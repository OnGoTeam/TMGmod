using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Cases")]
    public class hpodarok : Holdable, IPlatform
    {
        private System.Type _contains;

        public hpodarok (float xval, float yval)
          : base(xval, yval)
        {
            this.graphic = new Sprite(GetPath("HeavyMillitaryCase"));
		    this.center = new Vec2(7f, 4f);
            this.collisionOffset = new Vec2(-7f, -4f);
            this.collisionSize = new Vec2(14f, 8f);
            this.depth = -0.5f;
            this.thickness = 0.0f;
            this.weight = 3f;
            this.collideSounds.Add("presentLand");
            this._editorName = "Heavy Millitary Container";
        }


		public override void Initialize()
	{
		List<Type> things = new List<Type>()
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
        typeof(scargl),	
        typeof(SNR22),	
        typeof(SV99),	
        typeof(SVU),	
        typeof(SVUC),		
        typeof(TR21),	
        typeof(TR21C),	
        typeof(Vintorez),
        typeof(VintorezC),
        typeof(X3X)
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