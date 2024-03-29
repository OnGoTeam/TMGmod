﻿using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [BaggedProperty("canSpawn", false)]
    [UsedImplicitly]
    public class Cfour : Holdable
    {
        private Vec2 _stickyVec2;

        [UsedImplicitly] public bool Activated;

        [UsedImplicitly] public StateBinding ActivatedBinding = new(nameof(Activated));

        [UsedImplicitly] public MaterialThing Activator;

        [UsedImplicitly] public StateBinding ActivatorBinding = new(nameof(Activator));

        [UsedImplicitly] public bool Didboom;

        [UsedImplicitly] public StateBinding DidboomBinding = new(nameof(Didboom));

        [UsedImplicitly] public MaterialThing StickThing;

        [UsedImplicitly] public float ToExplode = -1f;

        [UsedImplicitly] public StateBinding ToExplodeBinding = new(nameof(ToExplode));

        [UsedImplicitly] public bool WasThrown;

        [UsedImplicitly] public StateBinding WasThrownBinding = new(nameof(WasThrown));

        [UsedImplicitly] public bool Weak;

        public Cfour(float xpos, float ypos) : base(xpos, ypos)
        {
            _weight = 3f;
            _graphic = new Sprite(GetPath("cfour"));
            _center = new Vec2(3f, 1.5f);
            _collisionOffset = new Vec2(-1.5f, -1.5f);
            _collisionSize = new Vec2(3f, 3f);
            flammable = 0.9f;
            thickness = 1f;
            throwSpeedMultiplier = 1.5f;
            airFrictionMult = 0.05f;
        }

        private void Explode()
        {
            if (Didboom) return;
            Didboom = true;
            Activator?.Fondle(this);
            Graphics.FlashScreen();
            for (var index = 0; index < 1; ++index)
            {
                var explosionPart = new ExplosionPart(x + Rando.Float(-8f, +8f), y + Rando.Float(-8f, +8f));
                explosionPart.scale *= 0.7f;
                Level.Add(explosionPart);
            }

            SFX.Play("explode");
            if (!isServerForObject || Weak) return;


            var varBullets = new List<Bullet>();
            for (var index = 0; index < 150; ++index)
            {
                var num = (float)(index * 30.0 - 10.0) + Rando.Float(20f);
                var atShrapnel = new ATShrapnel { range = 30f + Rando.Float(0f, Rando.Float(70f)) };
                var bullet = new Bullet(x + (float)(Math.Cos(Maths.DegToRad(num)) * 8.0),
                        y - (float)(Math.Sin(Maths.DegToRad(num)) * 8.0), atShrapnel, num)
                    { firedFrom = this };
                varBullets.Add(bullet);
                Level.Add(bullet);
            }

            if (Network.isActive)
            {
                Send.Message(new NMExplodingProp(varBullets), NetMessagePriority.ReliableOrdered);
                varBullets.Clear();
            }

            foreach (var window in Level.CheckCircleAll<Window>(position, 40f))
                if (Level.CheckLine<Block>(position, window.position, window) is null)
                    window.Destroy(new DTImpact(this));
            foreach (var thing in Level.CheckCircleAll<Thing>(position, 500f))
            {
                if (Level.CheckLine<Block>(position, thing.position, thing) is { }) continue;
                //else
                var dVec2 = thing.position - position + new Vec2(Rando.Float(-6f, 6f), Rando.Float(-6f, 6f));
                var l = dVec2.length + 0.1f;
                var force = dVec2 * (1000f / (l * l * l));
                //force.y *= 0.8f;
                //force.x *= 1.1f;
                thing.ApplyForce(force);
            }

            AddFire();
            Level.Remove(this);
        }

        public override void UpdateOnFire()
        {
            ToExplode = Rando.Float(0f, 1.5f);
            base.UpdateOnFire();
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            ToExplode = Rando.Float(0f, 0.7f);
            return base.Hit(bullet, hitPos);
        }

        public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnImpact(with, from);
            if (StickThing is { }) return;
            if (!Activated) return;
            StickThing = with;
            StickThing.Fondle(this);
            _stickyVec2 = with.ReverseOffset(position);
            _stickyVec2.x *= with.offDir;
            //enablePhysics = false;
            // ReSharper disable once SwitchStatementMissingSomeCases
            angleDegrees = from switch
            {
                ImpactedFrom.Right => -90f,
                ImpactedFrom.Bottom => 0f,
                ImpactedFrom.Left => 90f,
                ImpactedFrom.Top => 180f,
                _ => angleDegrees,
            };
        }

        public override void Thrown()
        {
            WasThrown = true;
            base.Thrown();
        }

        public override void Update()
        {
            if (_destroyed) return;
            if (StickThing is { _destroyed: true }) StickThing = null;

            ToExplode -= 0.1f;
            if (duck is { } && duck.holdObject == this)
            {
                StickThing = null;
                if (duck != Activator) Activated = false;
            }

            if (StickThing is { })
            {
                sleeping = true;
                position = StickThing.Offset(_stickyVec2);
                hSpeed = StickThing.hSpeed;
                vSpeed = StickThing.vSpeed;
            }
            else
            {
                sleeping = false;
            }

            if (Activator is Duck { dead: false } duck1 && duck1.IsQuacking()) ToExplode = Rando.Float(0f, 0.5f);

            if (grounded) angle = 0f;
            else if (duck?.holdObject != this && WasThrown && StickThing is null)
                angle += 0.3f * offDir;

            if (-0.5 < ToExplode && ToExplode < 0f && !_destroyed) Destroy();

            base.Update();
        }

        public override void OnPressAction()
        {
            if (duck is null) return;
            //else
            Activated = true;
            Activator = duck;
        }

        protected override bool OnDestroy(DestroyType _ = null)
        {
            if (_destroyed) return true;
            Explode();
            return true;
        }
    }
}
