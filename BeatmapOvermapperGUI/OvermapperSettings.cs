using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeatmapOvermapperGUI
{
	class OvermapperSettings : INotifyPropertyChanged
	{
		private int minimumBPM;
		private int maximumBPM;

		public int MinimumBPM
		{
			get => minimumBPM;
			set
			{
				minimumBPM = value;
				OnPropertyChanged();
			}
		}

		public int MaximumBPM
		{
			get => maximumBPM;
			set
			{
				maximumBPM = value;
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
