using Coosu.Beatmap;
using ProcessMemoryDataFinder.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatmapOvermapperGUI.Models
{
	public struct BeatmapWrapper
	{
		private OsuFile _osuFile;

		public string FolderName { get; set; }
		public string OsuFileName { get; set; }
		public string BeatmapFolderPath => Path.Combine(Settings.SongsFolder, FolderName);
		public string FullPath => Path.Combine(BeatmapFolderPath, OsuFileName);

		public OsuFile OsuFile
		{
			get
			{
				if (_osuFile == null)
					_osuFile = OsuFile.ReadFromFile(FullPath);
				return _osuFile;
			}
		}

	}
}
