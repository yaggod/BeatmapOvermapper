using Coosu.Beatmap;
using OsuMemoryDataProvider;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace BeatmapOvermapperGUI
{
	class DisplayContext : INotifyPropertyChanged
	{
		#region Fields
		private static StructuredOsuMemoryReader _memoryReader = StructuredOsuMemoryReader.Instance;

		private DispatcherTimer _timer = new();
		private string? _backgroundPath;
		private string? _beatmapName;
		private string? _difficultyName;
		#endregion


		public DisplayContext()
		{ 
			_timer.Tick += (_, _) => UpdateBeatmapData();
			_timer.Interval = new TimeSpan(0, 0, 1);
			_timer.Start();
		}

#pragma warning disable CS8602
#pragma warning disable CS8601 
		private void UpdateBeatmapData()
		{
			try
			{
				_memoryReader.TryRead(_memoryReader.OsuMemoryAddresses.Beatmap);
				string beatmapFolder = Path.Combine(Settings.SongsFolder, _memoryReader.OsuMemoryAddresses.Beatmap.FolderName);
				string osuFilePath = _memoryReader.OsuMemoryAddresses.Beatmap.OsuFileName;
				var fullPath = Path.Combine(beatmapFolder, osuFilePath);
				OsuFile file = OsuFile.ReadFromFile(fullPath);
				var backgroundFile = file.Events.BackgroundInfo.Filename;
				BackgroundPath = Path.Combine(beatmapFolder, backgroundFile);
				BeatmapName = file.Metadata.Title;
				DifficultyName = file.Metadata.Version;
			}
			catch
			{ }
		}
#pragma warning restore CS8602 
#pragma warning restore CS8601 


		public string BackgroundPath
		{
			get => _backgroundPath ?? "";
			private set
			{
				_backgroundPath = value;
				OnPropertyChanged();
			}

		}

		public string BeatmapName
		{
			get => _beatmapName ?? "";
			set
			{
				_beatmapName = value;
				OnPropertyChanged();
			}
		}

		public string DifficultyName
		{
			get => _difficultyName ?? "";
			set
			{
				_difficultyName = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

	}
}
