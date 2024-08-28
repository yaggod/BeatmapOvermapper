using BeatmapOvermapper.Validators;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeatmapOvermapperGUI
{
	class OvermapperSettings : INotifyPropertyChanged
	{
		public int MinimumBPM
		{
			get => _bpmValidator.MinimumRequiredBPM;
			set
			{
				_bpmValidator.MinimumRequiredBPM = value;
				OnPropertyChanged();
			}
		}

		public int MaximumBPM
		{
			get => _bpmValidator.MaximumRequiredBPM;
			set
			{
				_bpmValidator.MaximumRequiredBPM = value;
				OnPropertyChanged();
			}
		}

		public int MaxLength
		{
			get => _lengthValidator.MaxLength;
			set
			{
				_lengthValidator.MaxLength = value;
				OnPropertyChanged();
			}
		}

		public bool LengthValidatorEnabled
		{
			get => _lengthValidator.IsEnabled;
			set
			{
				_lengthValidator.IsEnabled = value;
				OnPropertyChanged();
			}
		}

		public PatternValidationProcessor Processor { get; private set; }
		private BPMValidator _bpmValidator = new();
		private LengthValidator _lengthValidator = new();


		public OvermapperSettings()
        {
			Processor = new();
			Processor.AddValidator(_bpmValidator);
			Processor.AddValidator(_lengthValidator);
		}



        public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}

	}
}
