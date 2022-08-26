using System;
using System.Reflection;
using DuckGame;
using JetBrains.Annotations;
#if DEBUG
using System.Linq;
using System.IO;
using TMGmod.Core.WClasses;
#endif

namespace TMGmod.Core
{
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class TMGmod : Mod
    {
        public TMGmod()
        {
            Debug.Log("TMGmod instantiated");
        }

        static TMGmod()
        {
            Debug.Log("TMGmod ddls registering...");
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var assembly = Assembly.GetAssembly(typeof(TMGmod));
                var dllName = $"{new AssemblyName(args.Name).Name}.dll";
                var resourceName = assembly.GetManifestResourceNames().SingleOrDefault(name => name.Contains(dllName));
                if (resourceName is null)
                    return null;
                using var stream = assembly.GetManifestResourceStream(resourceName);
                if (stream is null)
                    return null;
                var assemblyData = new byte[stream.Length];
                _ = stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            };
            Debug.Log("TMGmod ddls registered.");
        }

        public override Priority priority => Priority.Normal;

        protected override void OnPreInitialize()
        {
        }

        protected override void OnPostInitialize()
        {
            MapPack.LoadMapPack(GetPath<TMGmod>("TMG Levels"));
#if DEBUG
            var placeables = ItemBox.GetPhysicsObjects(Editor.Placeables);
            placeables.RemoveAll(t => !t.IsSubclassOf(typeof(BaseGun)));
            var guns = placeables.Select(
                Editor.CreateThing
            ).Select(
                gun => gun as BaseGun
            ).Where(
                based => based != null
            );
            var lines = guns.Select(
                gun => gun.StatsLine().Select(s => s.Replace(".", ","))
            );
            lines = new[] { BaseGun.StatsHeader() }.Concat(lines);
            var joined = lines.Select(
                e => string.Join(";", e)
            );
            var text = string.Join(
                "\n",
                joined
            );
            Debug.Log($"BaseGun Count: {placeables.Count}");
            var stats = GetPath<TMGmod>("stats");
            Directory.CreateDirectory(stats);
            File.WriteAllText(
                Path.Combine(stats, "guns.csv"),
                text
            );
#endif
        }
    }
}
