using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatmapOvermapper
{
	public static class OszCreator
	{
		public static void GenerateAndAddOsz(string songsFolder, string beatmapFolder)
		{
			string generatedOsz = GenerateOsz(songsFolder, beatmapFolder);

			Process proc = new Process();
			proc.StartInfo.FileName = generatedOsz;
			proc.StartInfo.UseShellExecute = true;
			proc.Start(); 
		}

		private static string GenerateOsz(string songsFolder, string beatmapFolder)
		{
			using FileStream fileStream = new FileStream(beatmapFolder + ".osz", FileMode.Create);
			using ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true);
			string beatmapFullPath = Path.Combine(songsFolder, beatmapFolder);
			foreach (string osuFile in Directory.EnumerateFiles(beatmapFullPath))
			{
				if (Path.GetExtension(osuFile) != ".osu")
					continue;

				var entry = archive.CreateEntryFromFile(osuFile, Path.GetFileName(osuFile));
			}

			return fileStream.Name;
		}
	}
}
