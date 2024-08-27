using System.Diagnostics;
using System.IO;
using System.Windows;

namespace BeatmapOvermapperGUI
{
	static class Settings
	{
		static Settings()
		{
			try
			{
				var osuProcess = Process.GetProcessesByName("osu!")[0];
				var osuExecutable = osuProcess.MainModule.FileName;
				OsuDirectory = Path.GetDirectoryName(osuExecutable);
			}
			catch
			{
				MessageBox.Show("Osu is not running", "Cant find osu process", MessageBoxButton.OK, MessageBoxImage.Error);
				throw;
			}
		}

		public static string OsuDirectory { get; private set; }
		public static string SongsFolder => Path.Combine(OsuDirectory, "Songs");
	}
}
