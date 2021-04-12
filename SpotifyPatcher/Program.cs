using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using SpotifyPatcher.Properties;
using System.Runtime.InteropServices;

namespace SpotifyPatcher
{
	class Program
	{
		static string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		static string LibraryHandle = Path.Combine(AppDataFolder, "Spotify\\chrome_elf.dll");
		static string ConfigHandle = Path.Combine(AppDataFolder, "Spotify");

		static void Main(string[] args)
		{
			DateTime now = DateTime.Now;
			Console.Title = "Spotify Ads Remover - Moshi#0002";
			if (File.Exists(LibraryHandle))
			{
				File.Delete(LibraryHandle);
				Yo();

				Console.WriteLine("Removed : {0}", now.TimeOfDay);
				Console.ReadKey();
				return;

			}
			else
			{
				Console.WriteLine("An error occured, Press any key to exit...");
				Console.ReadKey();
				return;
			}
		}
		static void Yo()
		{
			File.WriteAllBytes(LibraryHandle, Resources.chrome_elf);
			WConfig inif = new WConfig(ConfigHandle + "\\config.ini");
			inif.Write("Config", "Log", "0");
			inif.Write("Config", "Skip_wpad", "0");
			inif.Write("Config", "Block_BannerOnly", "0");
		}
	}
	// Write Config File ><
        // Credit to https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
	class WConfig
	{
		private string filePath;

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
		public WConfig(string filePath)
		{
			this.filePath = filePath;
		}

		public void Write(string section, string key, string value)
		{
			WritePrivateProfileString(section, key, value.ToLower(), this.filePath);
		}

		public string Read(string section, string key)
		{
			StringBuilder SB = new StringBuilder(255);
			int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);
			return SB.ToString();
		}

		public string FilePath
		{
			get
			{
				return this.filePath;
			}
			set
			{
				this.filePath = value;
			}
		}
	}
}
