using Coosu.Beatmap.Sections.HitObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatmapOvermapper.Validators
{
	public class LengthValidator : PatternValidator
	{
		public int MaxLength { get; set; }
		public LengthValidator()
		{
		}

		public override bool IsValidPattern(IEnumerable<RawHitObject> pattern)
		{
			return pattern.Count() <= MaxLength;
		}
	}
}
