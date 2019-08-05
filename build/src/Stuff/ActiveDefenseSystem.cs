#if DEBUG
using DuckGame;
using System.Collections.Generic;
using System;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    public class ActiveDefenseSystem:Holdable
    {
        private readonly List<AdsHit> _hitPosList = new List<AdsHit>();
        private readonly List<Tracked> _trackeds = new List<Tracked>();
        private readonly List<Grenade> _grenades = new List<Grenade>();
        private int _ticks;
        private readonly float _range;
        private readonly int _trackTicks;
        [UsedImplicitly]
        public int Ammo = 5;
        public StateBinding AmmoBinding = new StateBinding(nameof(Ammo));
        private readonly int _simTracked;
        public ActiveDefenseSystem(float xpos, float ypos) : base(xpos, ypos)
        {
            _graphic = new Sprite(GetPath("ADS"));
            _center = new Vec2(7f, 8f);
            _collisionOffset = new Vec2(-7f, -8f);
            _collisionSize = new Vec2(14f, 16f);
            _range = 128f;
            _trackTicks = 10;
            _simTracked = 2;
            _editorName = "ADS";
        }

        public override void Update()
        {
            _ticks += 1;
            if (grounded)
            {
                var grenades = Level.CheckCircleAll<Grenade>(position, _range);
                foreach (var grenade in grenades)
                {
                    if (!_grenades.Contains(grenade))
                    {
                        _grenades.Add(grenade);
                        _trackeds.Add(new Tracked(grenade));
                    }
                }
                _grenades.RemoveAll(InvalidGrenade);
                if (_grenades.Count > _simTracked)
                {
                    _grenades.RemoveRange(_simTracked, _grenades.Count - _simTracked);
                }
                _trackeds.RemoveAll(InvalidTracked);
                foreach (var tracked in _trackeds)
                {
                    tracked.Inst.DoHeatUp(0.1f, tracked.Inst.position);
                    tracked.AddLev();
                }
                foreach (var tracked in _trackeds)
                {
                    if (tracked.TrLev > _trackTicks)
                    {
                        HitTracked(tracked);
                    }
                }
            }
            else
            {
                _grenades.Clear();
                _trackeds.Clear();
            }
            base.Update();
        }

        private bool OldHit(AdsHit adsHit)
        {
            return (_ticks - adsHit.Tick) > 30;
        }

        private bool InvalidTracked(Tracked tracked)
        {
            var grenade = tracked.Inst;
            return tracked.TrLev > _trackTicks || !_grenades.Contains(grenade);
        }

        private bool InvalidGrenade(Grenade grenade)
        {
            return Level.CheckLine<Block>(position, grenade.position) != null || grenade.destroyed || (grenade.position - position).Length() > _range + 16f;
        }

        public override void Draw()
        {
            _hitPosList.RemoveAll(OldHit);
            foreach (var tracked in _trackeds)
            {
                if (tracked.TrLev <= _trackTicks)
                {
                    Graphics.DrawLine(position, tracked.Inst.position, new Color(255, 0, 0));
                }
            }
            foreach (var hitPos in _hitPosList)
            {
                var tickspast = hitPos.Tick - _ticks;
                var color = new Color(255, 255, 255 + 4 * tickspast);
                Graphics.DrawLine(position, hitPos.Pos, color);
                Graphics.DrawLine(hitPos.Pos - new Vec2(tickspast / (float) Math.Sqrt(8)), hitPos.Pos + new Vec2(tickspast / (float) Math.Sqrt(8)), color);
                Graphics.DrawCircle(hitPos.Pos, (float) tickspast / 2, color);
            }
            base.Draw();
        }

        private void HitTracked(Tracked tracked)
        {
            if (Ammo == 0) return;
            if (Ammo < -1) return;
            SFX.Play("click");
            if (Ammo != -1)
            {
                Ammo -= 1;
            }
            var grenade = tracked.Inst;
            float maxcount = 20;
            maxcount /= Math.Max(maxcount * (grenade.position - position).Length() / (_range * 5), 1);
            for (var i = 0; i < maxcount; ++i)
            {
                Level.Add(SmallSmoke.New(grenade.x, grenade.y));
            }
            grenade._destroyed = true;
            Level.Remove(grenade);
            _hitPosList.Add(new AdsHit(grenade.position, _ticks));
        }

        public override ContextMenu GetContextMenu()
        {
            var contextMenu = base.GetContextMenu();
            contextMenu.AddItem(new ContextSlider("ZARYADY", null, new FieldBinding(this, "Ammo", -1, 20, 1), 1, "INF"));
            return contextMenu;
        }

        private struct AdsHit
        {
            internal Vec2 Pos;
            internal int Tick;
            public AdsHit(Vec2 pos, int tick)
            {
                Pos = pos;
                Tick = tick;
            }
        }

        private class Tracked
        {
            internal readonly Grenade Inst;
            internal int TrLev { get; private set; }

            internal Tracked(Grenade grenade)
            {
                Inst = grenade;
            }

            internal void AddLev()
            {
                TrLev += 1;
            }
        }

        public override BinaryClassChunk Serialize()
        {
            BinaryClassChunk binaryClassChunk = base.Serialize();
            binaryClassChunk.AddProperty("Ammo", Ammo);
            return binaryClassChunk;
        }

        public override bool Deserialize(BinaryClassChunk node)
        {
            base.Deserialize(node);
            Ammo = node.GetProperty<int>("Ammo");
            return true;
        }
    }
}

#endif