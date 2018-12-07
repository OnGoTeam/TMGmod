using System;
using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    public class Cfour : Holdable
    {
        private float _toexplode = -1f;
        private bool _activated;
        private Duck _activator;
        private MaterialThing _stickThing;
        private Vec2 _stickyVec2;
        private bool _wasThrown;
        public bool Weak;

        public Cfour(float xpos, float ypos) : base(xpos, ypos)
        {
            weight = 3f;
            graphic = new Sprite(GetPath("cfour"));
            center = new Vec2(3f, 1.5f);
            collisionOffset = new Vec2(-1.5f, -1.5f);
            collisionSize = new Vec2(3f, 3f);
            flammable = 0.9f;
            thickness = 1f;
            throwSpeedMultiplier = 1.5f;
            airFrictionMult = 0.05f;
        }

        private void Explode()
        {
            ExploCreator.CreateExplosion(position);
            Graphics.FlashScreen();
            if (isServerForObject && !Weak)
            {
                var grenade = new Grenade(x, y);
                for (var index = 0; index < 150; ++index)
                {
                    var num2 = (float) (index * 18.0 - 5.0) + Rando.Float(10f);
                    var atShrapnel = new ATShrapnel {range = 30f + Rando.Float(0f, Rando.Float(70f))};
                    var bullet = new Bullet(x + (float) (Math.Cos(Maths.DegToRad(num2)) * 6.0),
                        y - (float) (Math.Sin(Maths.DegToRad(num2)) * 6.0), atShrapnel, num2)
                    {
                        firedFrom = this
                    };
                    grenade.firedBullets.Add(bullet);
                    Level.Add(bullet);
                }

                foreach (var window in Level.CheckCircleAll<Window>(position, 40f))
                    if (Level.CheckLine<Block>(position, window.position, window) == null)
                        window.Destroy(new DTImpact(this));
                foreach (var thing in Level.CheckCircleAll<Thing>(position, 200f))
                {
                    if (Level.CheckLine<Block>(position, thing.position, thing) != null) continue;
                    //else
                    var dVec2 = thing.position - position + new Vec2(Rando.Float(-6f, 6f), Rando.Float(-6f, 6f));
                    var l = dVec2.length + 0.1f;
                    var force = dVec2 * (1000f / (l * l * l));
                    //force.y *= 0.8f;
                    //force.x *= 1.1f;
                    thing.ApplyForce(force);
                }
                grenade.bulletFireIndex += 120;
                if (Network.isActive)
                {
                    Send.Message(new NMFireGun(grenade, grenade.firedBullets, grenade.bulletFireIndex, false),
                        NetMessagePriority.ReliableOrdered);
                    grenade.firedBullets.Clear();
                }
                AddFire();
            }

            Level.Remove(this);
            _destroyed = true;
        }

        public override void UpdateOnFire()
        {
            _toexplode = Rando.Float(0f, 1.5f);
            base.UpdateOnFire();
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            _toexplode = Rando.Float(0f, 0.7f);
            return base.Hit(bullet, hitPos);
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            if (_stickThing != null) return;
            base.OnImpact(with, from);
            if (!_activated) return;
            _stickThing = with;
            _stickyVec2 = position - with.position;
            enablePhysics = false;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (from)
            {
                case ImpactedFrom.Right:
                    angleDegrees = -90f;
                    break;
                case ImpactedFrom.Bottom:
                    angleDegrees = 0f;
                    break;
                case ImpactedFrom.Left:
                    angleDegrees = 90f;
                    break;
                case ImpactedFrom.Top:
                    angleDegrees = 180f;
                    break;
            }
        }

        public override void Thrown()
        {
            _wasThrown = true;
            base.Thrown();
        }

        public override void Update()
        {
            if (_destroyed) return;
            _toexplode -= 0.1f;
            if (duck != null && duck.holdObject == this && _stickThing != null)
            {
                _stickThing = null;
                _activated = false;
            }
            if (_stickThing != null) position = _stickThing.position + _stickyVec2;

            if (_activator != null && _activator.inputProfile.Down("QUACK")) _toexplode = Rando.Float(0f, 0.5f);

            if (grounded) angle = 0f;
            else if ((duck == null || duck.holdObject != this) && _wasThrown && _stickThing == null)
            {
                angle += 0.3f * offDir;
            }

            if (-0.5 < _toexplode && _toexplode < 0f) Explode();

            base.Update();
        }

        public override void OnPressAction()
        {
            _activated = true;
            _activator = duck;
        }
    }
}