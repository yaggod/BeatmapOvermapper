using Coosu.Beatmap.Sections.HitObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatmapOvermapper.Validators
{
	public class BPMValidator : PatternValidator
	{
		public int MinimumRequiredBPM { get; set; }
		public int MaximumRequiredBPM { get; set; }
		public int MaximumRequiredDelta => 15000 / MinimumRequiredBPM;
		public int MinimumRequiredDelta => 15000 / MaximumRequiredBPM;
		public int DeltaTolerance => 1;

		public BPMValidator()
		{
		}

		public override bool IsValidPair(RawHitObject first, RawHitObject second)
		{
			bool isFirstObjectCircle = first?.RawType.HasFlag(RawObjectType.Circle) == true;
			if (!(isFirstObjectCircle)) // kind of null check
				return false;

			int offsetsDelta = second.Offset - first.Offset;
			return (offsetsDelta <= MaximumRequiredDelta + DeltaTolerance) && (offsetsDelta >= MinimumRequiredDelta - DeltaTolerance);
		}
	}
}
