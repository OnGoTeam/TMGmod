using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;
using DuckGame;
using JetBrains.Annotations;

// ReSharper disable RedundantOverriddenMember

 
namespace TMGmod.Core
{
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class TMGmod : Mod
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        // ReSharper disable once MemberCanBePrivate.Global
		internal static string AssemblyName { get; private set; }
		
		//Приоритет. Мод загружается раньше/позже других модов
		public override Priority priority => base.priority;

        //Происходит перед запуском мода
        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();
        }
        //Происходит после запуска мода
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
		private void CreatingTMGLevels()
        {
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

            //Удаляем старые карты
            /*var oldlevels1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\New TMG Maps (PLS DONT USE OLD MAPS)\\");
            if (Directory.Exists(oldlevels1)) Directory.Delete(oldlevels1);
            if (Directory.Exists("DuckGame\\Levels\\New TMG Maps")) Directory.Delete("DuckGame\\Levels\\New TMG Maps");
            if (Directory.Exists("DuckGame\\Levels\\New TMG Levels")) Directory.Delete("DuckGame\\Levels\\New TMG Levels");
            if (Directory.Exists("DuckGame\\Levels\\New TMG Maps (PLS DONT USE OLD MAPS)")) Directory.Delete("DuckGame\\Levels\\New TMG Maps (PLS DONT USE OLD MAPS)");
            if (Directory.Exists("DuckGame\\Levels\\TMG Maps v2.0")) Directory.Delete("DuckGame\\Levels\\TMG Maps v2.0");
            if (Directory.Exists("DuckGame\\Levels\\TMG Maps v2.1")) Directory.Delete("DuckGame\\Levels\\TMG Maps v2.1");
            if (Directory.Exists("DuckGame\\Levels\\TMG v2.1.1")) Directory.Delete("DuckGame\\Levels\\TMG v2.1.1");*/

            // Потом создаём плейлист
            string uffPlaylistLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\TMG.play");
            XElement playlistElement = new XElement("playlist");
            foreach (string s in levelslist)
            {
                XElement levelElement = new XElement("element", s.Replace('\\', '/'));
                playlistElement.Add(levelElement);
            }
            XDocument playlist = new XDocument();
            playlist.Add(playlistElement);
            string contents = playlist.ToString();
            if (string.IsNullOrWhiteSpace(contents))
                throw new Exception("Blank XML (" + uffPlaylistLocation + ")");
            if (!File.Exists(uffPlaylistLocation) || !File.ReadAllText(uffPlaylistLocation).Equals(contents))
            {
                File.WriteAllText(uffPlaylistLocation, contents);
                SaveAsPlay(uffPlaylistLocation);
            }
        }
        private void SaveAsPlay(string path)
        {
            if (MonoMain.disableCloud || MonoMain.cloudNoSave)
                return;
            File.ReadAllBytes(path);
        }
    }
}