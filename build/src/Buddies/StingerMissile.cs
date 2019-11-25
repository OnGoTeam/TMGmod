using System.Linq;
using JetBrains.Annotations;
#if DEBUG
using DuckGame;
using System;
using System.Collections.Generic;
using System.Numerics;
using MathNet.Numerics;

namespace TMGmod.Buddies
{
    [PublicAPI]
    [EditorGroup("TMG|DEBUG")]
    public class StingerMissile:Holdable
    {
        private List<Vec2> _pList = new List<Vec2>();
        private const float A = 0.22f;
        private Duck _target;
        private bool _activated;
        private int _ticks;
        public StingerMissile(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            _graphic = sprite;
            sprite.frame = 3;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            airFrictionMult = 0f;
            throwSpeedMultiplier = 5f;
            friction = 0;
        }

        public override void Update()
        {
            UpdateTarget();
            UpdateFlight();
            base.Update();
            _pList.Add(position);
            _ticks += 1;
        }

        private void UpdateTarget()
        {
            if (_target != null)
                if (Level.CheckLine<Block>(position, _target.position) != null || _target.dead || _target == owner)
                    _target = null;
            if (_target != null) return;
            //else
            var ducks = Level.CheckCircleAll<Duck>(position, 1000);
            foreach (var d in ducks)
            {
                if (Level.CheckLine<Block>(position, d.position) == null && d != owner && !d.dead)
                    _target = d;
            }
        }

        public override void OnPressAction()
        {
            _activated = true;
            base.OnPressAction();
        }

        protected override bool OnDestroy(DestroyType dtype = null)
        {
            if (dtype is DTImpact dti && dti.thing == this) return true;
            new ATMissileShrapnel().MakeNetEffect(position);
            Random random = null;
            if (Network.isActive && isLocal)
            {
                random = Rando.generator;
                Rando.generator = new Random(NetRand.currentSeed);
            }
            var varBullets = new List<Bullet>();
            for (var index = 0; index < 12; ++index)
            {
                var num = (float)(index * 30.0 - 10.0) + Rando.Float(20f);
                var atMissileShrapnel = new ATMissileShrapnel { range = 15f + Rando.Float(5f) };
                var vec2 = new Vec2((float)Math.Cos(Maths.DegToRad(num)), (float)Math.Sin(Maths.DegToRad(num)));
                var bullet = new Bullet(x + vec2.x * 8f, y - vec2.y * 8f, atMissileShrapnel, num) { firedFrom = this };
                varBullets.Add(bullet);
                Level.Add(bullet);
                Level.Add(Spark.New(x + Rando.Float(-8f, 8f), y + Rando.Float(-8f, 8f), vec2 + new Vec2(Rando.Float(-0.1f, 0.1f), Rando.Float(-0.1f, 0.1f))));
                Level.Add(SmallSmoke.New(x + vec2.x * 8f + Rando.Float(-8f, 8f), y + vec2.y * 8f + Rando.Float(-8f, 8f)));
            }
            if (Network.isActive && isLocal)
            {
                Send.Message(new NMFireGun(null, varBullets, 0, false), NetMessagePriority.ReliableOrdered);
                varBullets.Clear();
            }
            if (Network.isActive && isLocal)
                Rando.generator = random;
            foreach (var window in Level.CheckCircleAll<DuckGame.Window>(position, 30f))
            {
                if (isLocal)
                    Fondle(window, DuckNetwork.localConnection);
                if (Level.CheckLine<Block>(position, window.position, window) == null)
                    window.Destroy(new DTImpact(this));
            }
            foreach (var physicsObject in Level.CheckCircleAll<PhysicsObject>(position, 70f))
            {
                if (isLocal && owner == null)
                    Fondle(physicsObject, DuckNetwork.localConnection);
                if ((physicsObject.position - position).length < 30.0)
                    physicsObject.Destroy(new DTImpact(this));
                physicsObject.sleeping = false;
                physicsObject.vSpeed = -2f;
            }
            var varBlocks = new HashSet<ushort>();
            foreach (var blockGroup1 in Level.CheckCircleAll<BlockGroup>(position, 50f))
            {
                if (blockGroup1 == null) continue;
                var blockGroup2 = blockGroup1;
                foreach (var block in blockGroup2.blocks.Where(block => Collision.Circle(position, 28f, block.rectangle)))
                {
                    block.shouldWreck = true;
                    if (block is AutoBlock autoBlock)
                        varBlocks.Add(autoBlock.blockIndex);
                }
                blockGroup2.Wreck();
            }
            foreach (var block in Level.CheckCircleAll<Block>(position, 28f))
            {
                switch (block)
                {
                    case AutoBlock autoBlock:
                        autoBlock.skipWreck = true;
                        autoBlock.shouldWreck = true;
                        varBlocks.Add(autoBlock.blockIndex);
                        break;
                    case Door _:
                    case VerticalDoor _:
                        Level.Remove(block);
                        block.Destroy(new DTRocketExplosion(null));
                        break;
                }
            }
            if (Network.isActive && isLocal && varBlocks.Count > 0)
                Send.Message(new NMDestroyBlocks(varBlocks));
            Level.Remove(this);
            return true;
        }

        /*public override void OnImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnImpact(with, from);
            if (!_activated) return;
            Destroy(new DTImpact(with));
        }*/

        private void UpdateFlight()
        {
            if (!_activated) return;
            //velocity += OffsetLocal(new Vec2(A, 0));
            if (_target is null) return;
            var p = _target.position - position;
            var v = _target.velocity - velocity;
            var g = new Vec2(0, gravity);
            var pa = Delta(-v, p, g);
            var maybeangle = Math.Acos(pa.x / pa.length);
            if (pa.y < 0) maybeangle = -maybeangle;
            if (offDir < 0) maybeangle += Math.PI;
            angle = (float) maybeangle;
            velocity += pa;
        }

        public override void Draw()
        {
            if (_target != null)
                Graphics.DrawCircle(_target.position, 16, Color.Red, depth: _target.depth.value + 3f);
            base.Draw();
            var p0 = position;
            _pList.Reverse();
            foreach (var pi in _pList)
            {
                Graphics.DrawLine(p0, pi, Color.Crimson);
                p0 = pi;
            }
            _pList.Reverse();
            if (_target is null) return;
            var p = _target.position - position;
            var v = _target.velocity - velocity;
            var g = new Vec2(0, gravity);
            var pa = Delta(-v, p, g);
            p0 = position;
            var v0 = velocity;
            for (var i = 0; i < 60; i++)
            {
                var pi = p0 + v0;
                Graphics.DrawLine(p0, pi, Color.Green);
                v0 += pa + g;
                p0 = pi;
            }
        }

        public override void Touch(MaterialThing with)
        {
            if (with is Duck duck0)
            {
                duck0.Kill(new DTImpact(this));
                //duck0._jumpValid = 4;
                //duck0._groundValid = 9999;
            }
            base.Touch(with);
        }


        private float Time5(float aa, Vec2 v, Vec2 p, Vec2 g, float t)
        {
            var res = 0f;
            var gg = Vec2.Dot(g, g);
            var vg = Vec2.Dot(v, g);
            var vv = Vec2.Dot(v, v);
            var pg = Vec2.Dot(p, g);
            var pv = Vec2.Dot(p, v);
            var pp = Vec2.Dot(p, p);
            res += gg - aa;
            res *= t;
            res += 4 * vg;
            res *= t;
            res += 4 * (vv - pg);
            res *= t;
            res += -8 * pv;
            res *= t;
            res += 4 * pp;
            return res;
        }

        private float Time4(float aa, Vec2 v, Vec2 p, Vec2 g)
        {
            /*
            double gg = Vec2.Dot(g, g);
            double vg = Vec2.Dot(v, g);
            double vv = Vec2.Dot(v, v);
            double pg = Vec2.Dot(p, g);
            double pv = Vec2.Dot(p, v);
            double pp = Vec2.Dot(p, p);
            var rar = FindRoots.Polynomial(new []
            {
                gg - aa,
                4 * vg,
                4 * (vv - pg),
                -8 * pv,
                4 * pp
            });*/
            var lf = new Func<float, float>(t => Time5(aa, v, p, g, t));
            var tu = 1e+6f * (float) Math.Sqrt(p.length) / Math.Max(A - gravity, 0.001f);
            var tb = 0f;
            for (var i = 0; i < 5000; i++)
            {
                var ti = (tb + tu) / 2;
                if (lf(ti) > 0)
                    tb = ti;
                else
                    tu = ti;
            }

            return tb;
        }

        private Vec2 Delta(Vec2 v, Vec2 p, Vec2 g)
        {
            const float aa = A * A;
            var t = Time4(aa, v, p, g);
            const float e = 1e-6f;
            if (t < e)
            {
                throw new DivideByZeroException();
                /*var av0 = -g;
                av0 /= g.length;
                av0 *= A;
                return av0;*/
            }

            var av = 2 * p - 2 * v * t - g * t * t;
            av /= t * t;
            return av;
        }
    }
}

#endif