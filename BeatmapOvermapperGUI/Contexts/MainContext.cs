namespace BeatmapOvermapperGUI.Contexts
{
    class MainContext
    {
        public DisplayContext Display
        { get; } = new();

        public CommandsContext Commands
        { get; } = new();

        public OvermapperSettings OvermapperSettings
        { get; } = new();
    }

}
