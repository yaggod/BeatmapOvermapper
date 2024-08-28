using BeatmapOvermapper;
using BeatmapOvermapperGUI.WarningHandling;
using OsuMemoryDataProvider;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Input;

namespace BeatmapOvermapperGUI.Commands
{
	class CreateCommand : ICommand
	{
		private static StructuredOsuMemoryReader _memoryReader = StructuredOsuMemoryReader.Instance;

		public event EventHandler? CanExecuteChanged;

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object? parameter)
		{
			OvermapperSettings settings = parameter as OvermapperSettings ?? throw new ArgumentException();
			BeatmapOvermapper.Overmapper overmapper = new(settings.MinimumBPM, settings.MaximumBPM);
			CreateMap(overmapper);
		}

		private static void CreateMap(Overmapper overmapper)
		{
			try
			{
				_memoryReader.TryRead(_memoryReader.OsuMemoryAddresses.Beatmap);
				string folderName = _memoryReader.OsuMemoryAddresses.Beatmap.FolderName;
				string beatmapFolder = Path.Combine(Settings.SongsFolder, folderName);
				string osuFilePath = _memoryReader.OsuMemoryAddresses.Beatmap.OsuFileName;

				string fullPath = Path.Combine(beatmapFolder, osuFilePath);
				overmapper.Overmap(fullPath);
				OszCreator.GenerateAndAddOsz(Settings.SongsFolder, folderName);
			}
			catch(Exception ex)
			{
				ShowMessageBox.ShowMessage(ex);
			}

		}
	}
}
