using System;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    public class MagBuddy
    {
        private readonly Gun _gun;
        private readonly Type _magType;
        private bool _loaded;
        public MagBuddy(Gun gun, Type magType=null, bool loaded=true)
        {
            _gun = gun;
            _magType = magType;
            _loaded = loaded;
        }
        [UsedImplicitly]
        public bool Disload()
        {
            if (!(_gun is ISupportReload gun)) return false;
            //else
            if (!_loaded || _magType == null) return true;
            //else
            if (!_magType.IsSubclassOf(typeof(Thing))) return false;
            //else
            var mag0 = Editor.CreateThing(_magType);
            mag0.position = _gun.Offset(gun.SpawnPos);
            mag0.offDir = _gun.offDir;
            Level.Add(mag0);
            if (gun.DropMag(mag0))
            {
                _loaded = false;
                return true;
            }
            //else
            Level.Remove(mag0);
            return false;
        }
        [UsedImplicitly]
        public bool Doload()
        {
            return !_loaded && _gun is ISupportReload gun && (_loaded = gun.SetMag());
        }

        [UsedImplicitly]
        public interface ISupportReload
        {
            bool SetMag();
            [UsedImplicitly]
            bool DropMag(Thing mag);

            //int TicksToR { get; set; }

            Vec2 SpawnPos { get; }
        }
    }
}