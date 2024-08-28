using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeatmapOvermapperGUI.WarningHandling
{
	public static class ShowMessageBox
	{
		public static void ShowMessage(Exception ex)
		{
			MessageBox.Show("Unknown error occured during beatmap creation. Report this issue on my github");
			MessageBox.Show(ex.ToString());
		}
	}
}
