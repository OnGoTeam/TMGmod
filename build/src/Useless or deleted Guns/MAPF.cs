#if DEBUG
using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor


namespace TMGmod.Useless_or_deleted_Guns
{
    [EditorGroup("TMG|DEBUG")]
    [BaggedProperty("canSpawn", false)]
    [PublicAPI]
    [Obsolete]
    // ReSharper disable once InconsistentNaming
    public class MAPF : Gun
    {
        public MAPF(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new AT9mm
            {
                combustable = true,
                range = 0f,
                accuracy = 1f,
                penetration = 2f,
            };
            _numBulletsPerFire = 2;
            _type = "gun";
            _graphic = new Sprite(GetPath("Mixaly4sPistol2"));
            center = new Vec2(7f, 7f);
            collisionOffset = new Vec2(-6.5f, -7f);
            collisionSize = new Vec2(15f, 12f);
            _barrelOffsetTL = new Vec2(15f, 3f);
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 0.4f;
            _kickForce = 0.5f;
            loseAccuracy = 0.5f;
            maxAccuracyLost = 0.8f;
            _holdOffset = new Vec2(-2f, 3f);
            _editorName = "FEUERFREI";
            weight = 2.5f;
        }

        public override void Fire()
        {
            if (ammo > 0)
            {
                //this.ammo--;
                ApplyKick();
                if (!receivingPress && isServerForObject)
                {
                    var pos = Offset(barrelOffset);
                    var d = new MF(pos.x, pos.y, this);
                    Fondle(d);
                    var travelDir = Maths.AngleToVec(barrelAngle);
                    d.hSpeed = travelDir.x * 14f;
                    d.vSpeed = travelDir.y * 14f;
                    Level.Add(d);
                }
            }

            base.Fire();
        }
    }
}
#endif
