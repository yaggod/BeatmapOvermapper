	using Coosu.Beatmap;
using Coosu.Beatmap.Sections.HitObject;
using System.Collections.Generic;
using System.Globalization;

namespace BeatmapOvermapper
{
	public class Overmapper
	{
		public float MinimumRequiredBPM { get; protected set; }
		public float MaximumRequiredBPM { get; protected set; }
		public float MaximumRequiredDelta => 15000 / MinimumRequiredBPM;
		public float MinimumRequiredDelta => 15000 / MaximumRequiredBPM;
		public int DeltaTolerance => 1;

		public Overmapper(float minimumRequiredBPM, float maximumRequiredBPM)
		{
			MinimumRequiredBPM = minimumRequiredBPM;
			MaximumRequiredBPM = maximumRequiredBPM;
		}

		public void Overmap(string beatmapPath)
		{
			OsuFile file = OsuFile.ReadFromFile(beatmapPath);
			string originalPath = Path.GetDirectoryName(beatmapPath);
			Overmap(file, originalPath);
		}

		private void Overmap(OsuFile beatmap, string originalPath)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

			var hitObjects = beatmap.HitObjects.HitObjectList;
			List<RawHitObject> objectsToAdd = new List<RawHitObject>();
			RawHitObject lastObject = null;
			foreach (var currentObject in hitObjects)
			{
				if (currentObject?.RawType.HasFlag(RawObjectType.Spinner) == true)
					continue;

				if (ShouldInsertNewNote(lastObject, currentObject))
				{
					var newObject = GetNewNote(lastObject, currentObject);
					objectsToAdd.Add(newObject);

				}
				lastObject = currentObject;
			}


			beatmap.HitObjects.HitObjectList.AddRange(objectsToAdd);
			beatmap.SaveToDirectory(originalPath, "[OVERMAPPED] " + beatmap.Metadata.Version);
		}

		private bool ShouldInsertNewNote(RawHitObject first, RawHitObject second)
		{
			bool isFirstObjectCircle = first?.RawType.HasFlag(RawObjectType.Circle) == true;
			if (!(isFirstObjectCircle)) // kind of null check
				return false;

			int offsetsDelta = second.Offset - first.Offset;
			return (offsetsDelta < MaximumRequiredDelta + DeltaTolerance) && (offsetsDelta > MinimumRequiredDelta - DeltaTolerance);
		}

		private RawHitObject GetNewNote(RawHitObject first, RawHitObject second)
		{
			RawHitObject newObject = new()
			{
				RawType = RawObjectType.Circle,
				Offset = (first.Offset + second.Offset) / 2,
				X = (first.X + second.X) / 2,
				Y = (first.Y + second.Y) / 2,

			};


			return newObject;
		}

	}
}
