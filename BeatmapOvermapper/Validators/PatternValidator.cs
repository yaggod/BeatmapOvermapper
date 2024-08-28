using Coosu.Beatmap.Sections.HitObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatmapOvermapper.Validators
{
    public abstract class PatternValidator
    {
        public bool IsEnabled { get; set; } = true;

        public virtual bool IsValidPair(RawHitObject first, RawHitObject second)
        {
            return true;
        }

        public virtual bool IsValidPattern(IEnumerable<RawHitObject> pattern)
        {
            return true;
        }

    }
}
