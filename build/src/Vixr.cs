using DuckGame;
using TMGmod.Core;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class Vixr : Gun
    {
		private bool stockngrip;

        public Vixr(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 25;
            _ammoType = new AT9mmS
            {
               range = 300f,
               accuracy = 0.78f,
               penetration = 1f,
               bulletSpeed = 21f
            };
            _type = "gun";
			//I'M BLUE DARUDE SANDSTORM DA DUBAI
            graphic = new Sprite(GetPath("VixrStock"));
            center = new Vec2(16.5f, 4.5f);
            collisionOffset = new Vec2(-16.5f, -4.5f);
            collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(34f, 4f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.65f;
            _kickForce = 0.8f;
            loseAccuracy = 0.099f;
            maxAccuracyLost = 0.17f;
            _editorName = "Vixr";
			weight = 3.9f;
		}
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK", false))
                    {
					  if (stockngrip)
					    {
				            graphic = new Sprite(GetPath("VixrStock"));
                            _ammoType.accuracy = 0.78f;
                            loseAccuracy = 0.099f;
		                 	stockngrip = false;
			                weight = 3.9f;
					    }
                      else
					    {
			   	            graphic = new Sprite(GetPath("VixrNoStock"));
                            _ammoType.accuracy = 0.74f;
                            loseAccuracy = 0.13f;
		                	stockngrip = true;
			                weight = 2f;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}