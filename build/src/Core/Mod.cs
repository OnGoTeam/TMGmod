using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

// ReSharper disable once CheckNamespace
namespace DuckGame.TMGmod
{
    // ReSharper disable once InconsistentNaming
    public class TMGmod : Mod
    {
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        internal static string AssemblyName { get; private set; }

        public TMGmod()
        {
            Debug.Log("TMGC loading");
        }

        // This function is run after all mods are loaded.
        protected override void OnPostInitialize()
		{
		
		    // sets the tmgmod external directory
            var tmgModDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TMGmod");
            if (!Directory.Exists(tmgModDirectory))
                Directory.CreateDirectory(tmgModDirectory);
			
            CreatingTMGLevels();
		}

        // ReSharper disable once InconsistentNaming
        private static IEnumerable<byte> GetMD5Hash(byte[] sourceBytes)
        {
            return new MD5CryptoServiceProvider().ComputeHash(sourceBytes);
        }

        // ReSharper disable once InconsistentNaming
        private static void CreatingTMGLevels()
        {
            // Сначала определяем левелы, и копируем их
            var levels = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\TMGC\\");
            if (!Directory.Exists(levels)) Directory.CreateDirectory(levels);
            IList<string> levelslist = new List<string>();

            foreach (var s in Directory.GetFiles(GetPath<TMGmod>("levels")))
            {
                var takefrom = AssemblyName + "\\content\\levels\\";
                var firstlocated = s.Replace('/', '\\');
                var copyto = levels + firstlocated.Substring(firstlocated.IndexOf(takefrom, StringComparison.Ordinal) + takefrom.Length);
                if (!File.Exists(copyto) || !GetMD5Hash(File.ReadAllBytes(firstlocated)).SequenceEqual(GetMD5Hash(File.ReadAllBytes(copyto)))) File.Copy(firstlocated, copyto, true);
                levelslist.Add(copyto);
            }
            // Потом создаём плейлист
            var tmgPlaylistLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\TMGC.play");
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
