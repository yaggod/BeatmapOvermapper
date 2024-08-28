using BeatmapOvermapperGUI.Models;
using OsuMemoryDataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatmapOvermapperGUI.Helpers
{
    public static class OsuMemoryReader
    {
        private static StructuredOsuMemoryReader _memoryReader = StructuredOsuMemoryReader.Instance;

        public static BeatmapWrapper GetCurrentBeatmap()
        {
            _memoryReader.TryRead(_memoryReader.OsuMemoryAddresses.Beatmap);
            string folderName = _memoryReader.OsuMemoryAddresses.Beatmap.FolderName;
            string osuFileName = _memoryReader.OsuMemoryAddresses.Beatmap.OsuFileName;

            return new BeatmapWrapper()
            {
                FolderName = folderName,
                OsuFileName = osuFileName
            };
        }
    }
}
