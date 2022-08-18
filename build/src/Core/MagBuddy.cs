using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;

namespace TMGmod.Core
{
    public class MagBuddy<T> where T : BaseGun, MagBuddy<T>.ISupportReload
    {
        private readonly T _gun;
        private readonly Type _magType;

        public MagBuddy(T gun, Type magType = null)
        {
            _gun = gun;
            _magType = magType;
        }

        [UsedImplicitly]
        public bool Disload()
        {
            if (!_gun.Loaded || _magType == null) return true;
            //else
            if (!_magType.IsSubclassOf(typeof(Thing))) return false;
            //else
            var mag0 = Editor.CreateThing(_magType);
            mag0.position = _gun.Offset(_gun.SpawnPos);
            mag0.velocity = _gun.velocity;
            mag0.offDir = _gun.offDir;
            Level.Add(mag0);
            if (_gun.DropMag(mag0))
            {
                _gun.Loaded = false;
                return true;
            }

            //else
            Level.Remove(mag0);
            return false;
        }

        [UsedImplicitly]
        public bool Doload()
        {
            return !_gun.Loaded && _gun is ISupportReload gun && (_gun.Loaded = gun.SetMag());
        }

        [UsedImplicitly]
        public interface ISupportReload
        {
            bool Loaded { get; set; }
            [UsedImplicitly] StateBinding MagLoadedBinding { get; }
            Vec2 SpawnPos { get; }
            bool SetMag();

            [UsedImplicitly]
            bool DropMag(Thing mag);
        }
    }
}
