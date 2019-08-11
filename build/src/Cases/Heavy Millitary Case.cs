using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Cases")]
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class hpodarok : Holdable, IPlatform
    {
        private Type _contains;

        public hpodarok (float xval, float yval)
          : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("HeavyMillitaryCase"));
		    _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Heavy Millitary Container";
        }


		public override void Initialize()
	{
		var things = new List<Type>()
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
		_contains = things[Rando.Int(things.Count - 1)];
	}

	public override void OnPressAction()
	{
		
		if (owner != null)
		{
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
			var newThing = Editor.CreateThing(_contains) as Holdable;
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