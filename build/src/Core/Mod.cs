using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;

// ReSharper disable RedundantOverriddenMember

// The title of your mod, as displayed in menus
[assembly: AssemblyTitle("TMG Mod")]

// The author of the mod
[assembly: AssemblyCompany("OGT")]

// The description of the mod
[assembly: AssemblyDescription("Current update: Holy Rework")]

// The mod's version
[assembly: AssemblyVersion("1.1.0.3")]

// ReSharper disable once CheckNamespace
namespace DuckGame.TMGmod
{
    // ReSharper disable once InconsistentNaming
    public class TMGmod : Mod
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        // ReSharper disable once MemberCanBePrivate.Global
		internal static string AssemblyName { get; private set; }
		
		// The mod's priority; this property controls the load order of the mod.
		public override Priority priority => base.priority;

        // This function is run before all mods are finished loading.
		protected override void OnPreInitialize()
		{
			base.OnPreInitialize();
		}
		
		// This function is run after all mods are loaded.
        protected override void OnPostInitialize()
		{
		
		    // sets the tmgmod external directory
            var tmgModDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TMGmod");
            if (!Directory.Exists(tmgModDirectory))
                Directory.CreateDirectory(tmgModDirectory);
			
			 CreateTMGLevelPlaylist();
		}

		private byte[] GetMD5Hash(byte[] sourceBytes)
        {
            return new MD5CryptoServiceProvider().ComputeHash(sourceBytes);
        }

        // ReSharper disable once InconsistentNaming
		private void CreateTMGLevelPlaylist()
		{
            var levelsLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\TMG v2.1.1\\");
            if (!Directory.Exists(levelsLocation))
                Directory.CreateDirectory(levelsLocation);
            IList<string> levelList = new List<string>();

            // copy levels over
            foreach (var s in Directory.GetFiles(GetPath<TMGmod>("levels")))
            {
                var saveString = AssemblyName + "\\content\\levels\\";
                var oldLocation = s.Replace('/', '\\');
                var newLocation = levelsLocation + oldLocation.Substring(oldLocation.IndexOf(saveString, StringComparison.Ordinal) + saveString.Length);
                if (!File.Exists(newLocation) || !GetMD5Hash(File.ReadAllBytes(oldLocation)).SequenceEqual(GetMD5Hash(File.ReadAllBytes(newLocation))))
                    File.Copy(oldLocation, newLocation, true);
                levelList.Add(newLocation);
			}
			// create playlist
            var tmgPlaylistLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\TMGlev.play");
            var playlistElement = new XElement("playlist");
            foreach (var s in levelList)
            {
                var levelElement = new XElement("element", s.Replace('\\', '/'));
                playlistElement.Add(levelElement);
            }
            var playlist = new XDocument();
            playlist.Add(playlistElement);
            var contents = playlist.ToString();
            if (string.IsNullOrWhiteSpace(contents))
                throw new Exception("Blank XML (" + tmgPlaylistLocation + ")");
        }
	}
}
