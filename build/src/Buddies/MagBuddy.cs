#if DEBUG
using System;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Buddies
{
    [PublicAPI]
    public class MagBuddy
    {
        private readonly Gun _gun;
        private readonly Type _magType;

        [PublicAPI]
        public MagBuddy(Gun gun, Type magType)
        {
            _gun = gun;
            _magType = magType;
        }

        [PublicAPI]
        public bool Reload()
        {
            if (!(_gun is ISupportReload gun)) return false;
            if (!_magType.IsSubclassOf(typeof(Thing))) return false;
            var mag0 = Editor.CreateThing(_magType);
            mag0.position = _gun.position;
            mag0.position += new Vec2(gun.SpawnPos.x * _gun.offDir, gun.SpawnPos.y);
            mag0.offDir = _gun.offDir;
            Level.Add(mag0);
            return gun.DropMag(mag0) && gun.SetMag();
        }


        /// <summary>
        /// interface for guns
        /// </summary>
        [PublicAPI]
        public interface ISupportReload
        {
            /// <summary>
            /// is called when reload finishes
            /// </summary>
            bool SetMag();

            bool DropMag(Thing mag);

            //int TicksToR { get; set; }

            Vec2 SpawnPos { get; set; }
        }
    }
}
#endif