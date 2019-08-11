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
    public class warriorscase : Holdable, IPlatform
    {
        private Type _contains;
	    private readonly SpriteMap _sprite;

        public warriorscase (float xval, float yval)
          : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("WarriorsCase"), 14, 8);
            _graphic = _sprite;	
		    _sprite.frame = Rando.Int(0, 4);
		    _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Warriors Case";
        }


		public override void Initialize()
	{
		var things = new List<Type>()
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