using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Properties;


namespace TMGmod.Core
{
    /// <inheritdoc />
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class TMGmod : Mod
    {
        [UsedImplicitly]
        internal string Bdate = Resources.BuildDate;
        [UsedImplicitly]
        internal static TMGmod LastInstance;
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        // ReSharper disable once MemberCanBePrivate.Global
        /// <inheritdoc />
        public TMGmod()
        {
            Debug.Log("TMGmod loading");
            AppDomain.CurrentDomain.AssemblyResolve +=
                CurrentDomain_AssemblyResolve;
            LastInstance = this;
        }
        [UsedImplicitly]
        internal static string AssemblyName { get; private set; }
		
		//Приоритет. Мод загружается раньше/позже других модов
        /// <inheritdoc />
        public override Priority priority => Priority.Normal;

        //Происходит перед запуском мода
        /*protected override void OnPreInitialize()
        {
            base.OnPreInitialize();
        }*/

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyname = new AssemblyName(args.Name).Name;
            var assemblyFileName = Path.Combine(configuration.directory, assemblyname + ".dll");
            var assembly = Assembly.LoadFrom(assemblyFileName);
            return assembly;
        }

        //Происходит после запуска мода
        /// <inheritdoc />
        protected override void OnPostInitialize()
        {
            //Директория
            var tmgModDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TMGmod");
            if (!Directory.Exists(tmgModDirectory))
                Directory.CreateDirectory(tmgModDirectory);

            CreatingTMGLevels();
            base.OnPostInitialize();
        }

        // ReSharper disable once InconsistentNaming
        private static IEnumerable<byte> GetMD5Hash(byte[] sourceBytes)
        {
            return new MD5CryptoServiceProvider().ComputeHash(sourceBytes);
        }

        // ReSharper disable once InconsistentNaming
        // Чтобы играть было приятно, пихаем карты в сам мод, и делаем так, чтобы они скачивались вместе с ним
		private static void CreatingTMGLevels()
        {
            //Уносим старые нерабочие карты в очко
            var olddirlist = new List<string>(new[]
            {
                "DuckGame\\Levels\\New TMG Maps",
                "DuckGame\\Levels\\New TMG Levels",
                "DuckGame\\Levels\\New TMG Maps (PLS DONT USE OLD MAPS)",
                "DuckGame\\Levels\\TMG",
                "DuckGame\\Levels\\TMG Maps v2.0",
                "DuckGame\\Levels\\TMG Maps v2.1",
                "DuckGame\\Levels\\TMG v2.1.1"
            });
            foreach (var dirpath1 in olddirlist.Select(dirpath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), dirpath)).Where(Directory.Exists))
            {
                Directory.Delete(dirpath1, true);
            }
            // Сначала определяем левелы, и копируем их
            var levels = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\TMG\\");
            if (!Directory.Exists(levels)) Directory.CreateDirectory(levels);
            IList<string> levelslist = new List<string>();

            foreach (var s in Directory.GetFiles(GetPath<TMGmod>("maps")))
            {
                var takefrom = AssemblyName + "\\content\\maps\\";
                var firstlocated = s.Replace('/', '\\');
                var copyto = levels + firstlocated.Substring(firstlocated.IndexOf(takefrom, StringComparison.Ordinal) + takefrom.Length);
                if (!File.Exists(copyto) || !GetMD5Hash(File.ReadAllBytes(firstlocated)).SequenceEqual(GetMD5Hash(File.ReadAllBytes(copyto)))) File.Copy(firstlocated, copyto, true);
                levelslist.Add(copyto);
            }
            // Потом создаём плейлист
            var tmgPlaylistLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\TMG.play");
            var playlistElement = new XElement("playlist");
            foreach (var s in levelslist)
            {
                var levelElement = new XElement("element", s.Replace('\\', '/'));
                playlistElement.Add(levelElement);
            }
            var playlist = new XDocument();
            playlist.Add(playlistElement);
            var contents = playlist.ToString();
            if (string.IsNullOrWhiteSpace(contents))
                throw new Exception("Blank XML (" + tmgPlaylistLocation + ")");
            if (File.Exists(tmgPlaylistLocation) && File.ReadAllText(tmgPlaylistLocation).Equals(contents)) return;
            //else
            File.WriteAllText(tmgPlaylistLocation, contents);
            SaveAsPlay(tmgPlaylistLocation);
        }
        private static void SaveAsPlay(string path)
        {
            if (MonoMain.disableCloud || MonoMain.cloudNoSave)
                return;
            File.ReadAllBytes(path);
        }
    }
}