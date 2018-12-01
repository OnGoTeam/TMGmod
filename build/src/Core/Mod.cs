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
[assembly: AssemblyDescription("Current update: Release")]

// The mod's version
[assembly: AssemblyVersion("1.1.0.0")]

// ReSharper disable once CheckNamespace
namespace DuckGame.TMGmod
{
    // ReSharper disable once InconsistentNaming
    public class TMGmod : Mod
    {
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
            string tmgModDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TMGmod");
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
            string levelsLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\New TMG Maps (PLS DONT USE OLD MAPS)\\");
            if (!Directory.Exists(levelsLocation))
                Directory.CreateDirectory(levelsLocation);
            IList<string> levelList = new List<string>();

            // copy levels over
            foreach (string s in Directory.GetFiles(GetPath<TMGmod>("levels")))
            {
                string saveString = AssemblyName + "\\content\\levels\\";
                string oldLocation = s.Replace('/', '\\');
                string newLocation = levelsLocation + oldLocation.Substring(oldLocation.IndexOf(saveString) + saveString.Length);
                if (!File.Exists(newLocation) || !GetMD5Hash(File.ReadAllBytes(oldLocation)).SequenceEqual(GetMD5Hash(File.ReadAllBytes(newLocation))))
                    File.Copy(oldLocation, newLocation, true);
                levelList.Add(newLocation);
			}
			// create playlist
            string tmgPlaylistLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DuckGame\\Levels\\TMGlev.play");
            XElement playlistElement = new XElement("playlist");
            foreach (string s in levelList)
            {
                XElement levelElement = new XElement("element", s.Replace('\\', '/'));
                playlistElement.Add(levelElement);
            }
            XDocument playlist = new XDocument();
            playlist.Add(playlistElement);
            string contents = playlist.ToString();
            if (string.IsNullOrWhiteSpace(contents))
                throw new Exception("Blank XML (" + tmgPlaylistLocation + ")");
        }
	}
}
