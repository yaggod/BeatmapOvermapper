using Coosu.Beatmap;
using OsuMemoryDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeatmapOvermapperGUI.Commands
{
	internal class SelectBPMCommand : ICommand
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

			var beatmap = Helpers.OsuMemoryReader.GetCurrentBeatmap();
			OsuFile file = beatmap.OsuFile;
			settings.MaximumBPM = (int) file.TimingPoints.TimingList.Where(item => !item.IsInherit).Max(item => item.Bpm);
			settings.MinimumBPM = (int) file.TimingPoints.TimingList.Where(item => !item.IsInherit).Min(item => item.Bpm);
			
		}
	}	
}
