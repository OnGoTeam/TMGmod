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

        public Animating(Action<int, T> update, Action<T> end, bool nodata=false)
        {
            _update = update;
            _end = end;
            _nodata = nodata;
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

        public void Complete()
        {
            if (_frames > 0)
                Set(0);
        }

        public T Cancel()
        {
            _frames = 0;
            var data = _data;
            _data = default;
            return data;
        }

        public bool Active() => _frames > 0;

        public T Data() => _data;

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

        protected override void ModifyUpdate()
        {
            if (_frames > 0)
                Set(_frames - 1);
        }
    }

    public static class Anime {
        public static Animating<object> Simple(Action<int> update, Action end)
        {
            return new Animating<object>(
                (frame, _) => update(frame),
                _ => end(),
                true
            );
        }
    }
}
