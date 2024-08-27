using BeatmapOvermapper;
using OsuMemoryDataProvider;
using System.IO;
using System.Windows.Input;

namespace BeatmapOvermapperGUI.Commands
{
	class CreateCommand : ICommand
	{
		private static StructuredOsuMemoryReader _memoryReader = new();

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
			_memoryReader.TryRead(_memoryReader.OsuMemoryAddresses.Beatmap);
			string beatmapFolder = Path.Combine(Settings.SongsFolder, _memoryReader.OsuMemoryAddresses.Beatmap.FolderName);
			string osuFilePath = _memoryReader.OsuMemoryAddresses.Beatmap.OsuFileName;

			string fullPath = Path.Combine(beatmapFolder, osuFilePath);
			overmapper.Overmap(fullPath);
		}
	}
}
