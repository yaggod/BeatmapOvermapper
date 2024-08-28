using Coosu.Beatmap.Sections.HitObject;
using System.Collections.Generic;

namespace BeatmapOvermapper.Validators
{
    public class PatternValidationProcessor
    {
        private List<PatternValidator> _validators = new();

        public void AddValidator(PatternValidator validator)
        {
            _validators.Add(validator);
        }

        public virtual bool IsValidPair(RawHitObject first, RawHitObject second)
        {
            foreach (var validator in _validators)
            {
                if (!(validator.IsValidPair(first, second) || !validator.IsEnabled))
                    return false;
            }

            return true;
        }

        public virtual bool IsValidPattern(IEnumerable<RawHitObject> pattern)
        {
            if (pattern.Count() < 2)
                return false;
            foreach (var validator in _validators)
            {
                if (!(validator.IsValidPattern(pattern) || !validator.IsEnabled))
                    return false;
            }

            return true;
        }
    }
}
