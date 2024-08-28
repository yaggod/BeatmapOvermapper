using BeatmapOvermapperGUI.Commands;
using System.Windows.Input;

namespace BeatmapOvermapperGUI
{
	public class CommandsContext
	{
		public ICommand CreateCommand
		{ get; } = new CreateCommand();
		public ICommand SelectBPMCommand
		{ get; } = new SelectBPMCommand();
	}
}