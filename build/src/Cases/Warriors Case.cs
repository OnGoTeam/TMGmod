using System;
using System.Collections.Generic;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Cases")]
    public class warriorscase : Holdable, IPlatform
    {
        private Type _contains;
	    private SpriteMap _sprite;

        public warriorscase (float xval, float yval)
          : base(xval, yval)
        {
            _sprite = new SpriteMap((GetPath("WarriorsCase")), 14, 8, false);
            graphic = (Sprite)_sprite;	
		    _sprite.frame = Rando.Int(0, 4);
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
		List<Type> things = new List<Type>()
	    {
        typeof(usp),
        typeof(M93R),	
        typeof(M4A1),	
        typeof(SV99),
        typeof(usp),
        typeof(M93R),	
        typeof(M4A1),	
        typeof(SV99),
        typeof(usp),
        typeof(M93R),	
        typeof(M4A1),	
        typeof(SV99),
        typeof(usp),
        typeof(M93R),	
        typeof(M4A1),	
        typeof(SV99),	
        typeof(Vintorez),	
        typeof(ussrgun),
        typeof(MG44),	
        typeof(Vintorez),	
        typeof(ussrgun),
        typeof(MG44),	
        typeof(Vintorez),	
        typeof(ussrgun),
        typeof(MG44),	
        typeof(AN94),	
        typeof(cz805),	
        typeof(AN94),	
        typeof(cz805),	
        typeof(PPSh),	
        typeof(SVU)
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