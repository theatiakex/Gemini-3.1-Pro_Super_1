using System.Collections.Generic;
using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Parsers;

public class SrtParser : ISubtitleParser
{
    public IEnumerable<Cue> Parse(string rawContent)
    {
        return new List<Cue>();
    }
}