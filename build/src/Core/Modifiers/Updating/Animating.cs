using System;
using DuckGame;

namespace TMGmod.Core.Modifiers.Updating
{
    public class Animating<T> : Modifier
    {
        private readonly Action<int, T> _update;
        private readonly Action<T> _end;
        private int _frames;
        private T _data;
        private readonly bool _nodata;
        private readonly bool _block;

        public Animating(Action<int, T> update, Action<T> end, bool nodata = false, bool block = false)
        {
            _update = update;
            _end = end;
            _nodata = nodata;
            _block = block;
        }

        public void Set(int frames, T data)
        {
            _data = data;
            Set(frames);
        }

        public void Set(int frames)
        {
            _frames = frames;
            _update(frames, _data);
            if (frames <= 0)
                End();
        }

        private void End()
        {
            _end(_data);
            _data = default;
        }
#if DEBUG
        public void Complete()
#else
        private void Complete()
#endif
        {
            if (_frames > 0)
                Set(0);
        }
#if DEBUG
        public T Cancel()
#else
        public void Cancel()
#endif
        {
            _frames = 0;
#if DEBUG
            var data = _data;
#endif
            _data = default;
#if DEBUG
            return data;
#endif
        }

        public bool Active() => _frames > 0;
#if DEBUG
        public T Data() => _data;
#endif

        protected override void Write(BitBuffer buffer)
        {
            buffer.Write(_frames);
            if (!_nodata && _frames > 0) buffer.Write(_data);
        }

        protected override void Read(BitBuffer buffer)
        {
            var frames = buffer.ReadInt();
            if (frames > 0)
            {
                if (_nodata)
                    Set(frames);
                else
                {
                    var data = buffer.Read<T>();
                    Set(frames, data);
                }
            }
            else
                Complete();
        }

        private void Decrement()
        {
            if (_frames > 0)
                Set(_frames - 1);
        }

#if DEBUG
        public void Decrement(int n)
        {
            if (_frames > 0 && n > 0)
                Set(Math.Max(_frames - n, 0));
        }
#endif

        protected override void ModifyUpdate() => Decrement();

        public override bool CanFire() => !_block || !Active();
#nullable enable
        public Animating<T>? AsInactive() => !Active() ? this : null;
#nullable restore
    }

    public static class Anime
    {
        public static Animating<object> Simple(Action<int> update, Action end)
        {
            return new Animating<object>(
                (frame, _) => update(frame),
                _ => end(),
                nodata: true
            );
        }
    }
}
