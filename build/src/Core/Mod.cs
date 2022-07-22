using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class TMGmod : Mod
    {
        public TMGmod()
        {
            Debug.Log("TMGmod loading");
        }

        public override Priority priority => Priority.Normal;

        protected override void OnPostInitialize()
        {
            MapPack.LoadMapPack(GetPath<TMGmod>("TMG Levels"));
        }
    }
}
