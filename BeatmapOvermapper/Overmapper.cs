using BeatmapOvermapper.Validators;
using Coosu.Beatmap;
using Coosu.Beatmap.Sections.HitObject;
using System.Globalization;

namespace BeatmapOvermapper
{
	public class Overmapper
	{
		private readonly PatternValidationProcessor _processor;

		public Overmapper(PatternValidationProcessor processor)
		{
			_processor = processor;
		}

		public void Overmap(string beatmapPath)
		{
			OsuFile file = OsuFile.ReadFromFile(beatmapPath);
			string originalPath = Path.GetDirectoryName(beatmapPath);
			Overmap(file, originalPath);
		}

		private void Overmap(OsuFile beatmap, string originalPath)
		{
			List<List<RawHitObject>> validPatterns = GetPatternsToModify(beatmap);
			List<RawHitObject> objectsToAdd = GetNewNotes(validPatterns);
			beatmap.HitObjects.HitObjectList.AddRange(objectsToAdd);

			Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
			beatmap.GetHashCode();
			beatmap.SaveToDirectory(originalPath, "[OVERMAPPED] " + beatmap.Metadata.Version);
		}

		private List<List<RawHitObject>> GetPatternsToModify(OsuFile beatmap)
		{
			var hitObjects = beatmap.HitObjects.HitObjectList;
			List<List<RawHitObject>> validPatterns = new();
			List<RawHitObject> currentPattern = new();
			RawHitObject lastObject = null;
			foreach (var currentObject in hitObjects)
			{
				if (currentObject.RawType.HasFlag(RawObjectType.Spinner) == true)
					continue;
				if (lastObject == null)
				{
					lastObject = currentObject;
					continue;
				}


				if (_processor.IsValidPair(lastObject, currentObject))
				{
					if (!currentPattern.Any())
						currentPattern.Add(lastObject);
					currentPattern.Add(currentObject);

				}
				else
				{
					if (_processor.IsValidPattern(currentPattern))
					{
						validPatterns.Add(currentPattern);
						currentPattern = new();
					}
					else
					{
						currentPattern.Clear();
					}
				}
				lastObject = currentObject;
			}

			return validPatterns;
		}

		private List<RawHitObject> GetNewNotes(List<List<RawHitObject>> validPatterns)
		{
			List<RawHitObject> objectsToAdd = new();
			foreach (var pattern in validPatterns)
			{
				RawHitObject lastNote = pattern.First();
				foreach (var currentNote in pattern.Skip(1))
				{
					objectsToAdd.Add(GetNewNote(lastNote, currentNote));
					lastNote = currentNote;
				}
			}

			return objectsToAdd;
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
