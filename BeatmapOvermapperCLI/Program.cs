namespace BeatmapOvermapperCLI
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string filePath = args[0];
			new BeatmapOvermapper.Overmapper(150, 200).Overmap(filePath);
		}
	}
}
