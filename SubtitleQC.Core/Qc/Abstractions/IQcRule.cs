using System.Collections.Generic;
using SubtitleQc.Core.Models;

namespace SubtitleQc.Core.Qc.Abstractions;

public interface IQcRule
{
    string Name { get; }
    IEnumerable<QcResult> Execute(IEnumerable<Cue> cues);
}