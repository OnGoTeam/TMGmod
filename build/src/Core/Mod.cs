using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Properties;

namespace TMGmod.Core
{
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class TMGmod : Mod
    {
        [UsedImplicitly] internal static TMGmod LastInstance;

        [UsedImplicitly] internal string Bdate = Resources.BuildDate;

        public TMGmod()
        {
            Debug.Log("TMGmod loading");
            /*AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;*/
            LastInstance = this;
        }

        public override Priority priority => Priority.Normal;

        /*protected override void OnPreInitialize()
        {
            base.OnPreInitialize();
        }*/

        protected override void OnPostInitialize()
        {
            //Директория
            var tmgModDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TMGmod");
            if (!Directory.Exists(tmgModDirectory))
                Directory.CreateDirectory(tmgModDirectory);

            SetupPlaylist();
            base.OnPostInitialize();
        }

        // ReSharper disable once InconsistentNaming
        private static IEnumerable<byte> GetMD5Hash(byte[] sourceBytes)
        {
            return new MD5CryptoServiceProvider().ComputeHash(sourceBytes);
        }

        private static IEnumerable<string> SetupLevels()
        {
            var levelsTarget = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "DuckGame\\Levels\\TMG\\");
            if (!Directory.Exists(levelsTarget))
                Directory.CreateDirectory(levelsTarget);
            IList<string> levels = new List<string>();
            const string levelsSource = "\\content\\maps\\";
            foreach (var s in Directory.GetFiles(GetPath<TMGmod>("maps")))
            {
                var fileSource = s.Replace('/', '\\');
                var fileName = fileSource.Substring(fileSource.IndexOf(levelsSource, StringComparison.Ordinal) +
                                                    levelsSource.Length);
                var fileTarget = Path.Combine(levelsTarget, fileName);
                if (!File.Exists(fileTarget) || !GetMD5Hash(File.ReadAllBytes(fileSource))
                    .SequenceEqual(GetMD5Hash(File.ReadAllBytes(fileTarget))))
                    File.Copy(fileSource, fileTarget, true);
                levels.Add(fileTarget);
            }

            return levels;
        }

        private static XDocument Playlist(IEnumerable<string> levels)
        {
            var playlistElement = new XElement("playlist");
            foreach (var s in levels)
            {
                var levelElement = new XElement("element", s.Replace('\\', '/'));
                playlistElement.Add(levelElement);
            }

            return new XDocument(playlistElement);
        }

        private static void SavePlaylistToFile(XDocument playlist, string path)
        {
            var contents = playlist.ToString();
            if (string.IsNullOrWhiteSpace(contents))
                throw new Exception("Blank XML (" + path + ")");
            if (File.Exists(path) && File.ReadAllText(path).Equals(contents)) return;
            //else
            File.WriteAllText(path, contents);
            if (MonoMain.disableCloud || MonoMain.cloudNoSave)
                return;
            File.ReadAllBytes(path);
        }

        private static void SaveLevelsAsPlaylist(IEnumerable<string> levels)
        {
            SavePlaylistToFile(
                Playlist(levels),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    "DuckGame\\Levels\\TMG.play")
            );
        }

        // ReSharper disable once InconsistentNaming
        // Чтобы играть было приятно, пихаем карты в сам мод, и делаем так, чтобы они скачивались вместе с ним
        private static void SetupPlaylist()
        {
            // Сначала определяем левелы, и копируем их
            var levels = SetupLevels();
            // Потом создаём плейлист
            SaveLevelsAsPlaylist(levels);
        }
    }
}
