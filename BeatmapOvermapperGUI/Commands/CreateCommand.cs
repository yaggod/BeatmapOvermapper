using BeatmapOvermapper;
using BeatmapOvermapperGUI.Helpers;
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
				var beatmap = Helpers.OsuMemoryReader.GetCurrentBeatmap();

				overmapper.Overmap(beatmap.FullPath);
				OszCreator.GenerateAndAddOsz(Settings.SongsFolder, beatmap.FolderName);
			}
			catch(Exception ex)
			{
				ShowMessageBox.ShowMessage(ex);
			}

		}
	}
}
