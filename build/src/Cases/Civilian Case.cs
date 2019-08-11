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
    public class lpodarok : Holdable, IPlatform
    {
        private Type _contains;
	    private readonly SpriteMap _sprite;

        public lpodarok (float xval, float yval)
          : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("CivilianCase"), 14, 8);
            _graphic = _sprite;	
		    _sprite.frame = Rando.Int(0, 4);
		    _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Civilian Container";
        }


		public override void Initialize()
	{
		var things = new List<Type>()
	    {
        typeof(CZ75),
        typeof(AF2011),
        typeof(M93R),	
        typeof(MAP),	
        typeof(MPA27),	
        typeof(BigShot),	
        typeof(Nellegalja),
        typeof(scarl),	
        typeof(mk20),	
        typeof(SV98),	
        typeof(usp),	
        typeof(MAPFire),	
        typeof(bren)
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
        if (Editor.CreateThing(_contains) is Holdable newThing)
        {
            if (Rando.Int(500) == 1 && newThing is Gun thing && thing.CanSpawnInfinite())
            {
                thing.infiniteAmmoVal = true;
                thing.infinite.value = true;
            }
            newThing.x = o.x;
            newThing.y = o.y;
            Level.Add(newThing);
            if (d == null) return;
            //else
            d.GiveHoldable(newThing);
            d.resetAction = true;
        }
    }
    }
}