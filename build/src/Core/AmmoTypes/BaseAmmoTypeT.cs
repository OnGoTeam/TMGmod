using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.AmmoTypes
{
    /// <summary>
    ///     AmmoType with GetPath
    /// </summary>
    [PublicAPI]
    public abstract class BaseAmmoTypeT : AmmoType
    {
        /// <summary>
        ///     Implements GetPath for <see cref="AmmoType"/> through <see cref="Mod"/>.GetPath
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        protected string GetPath(string asset)
        {
            return Mod.GetPath<TMGmod>(asset);
        }
    }
}