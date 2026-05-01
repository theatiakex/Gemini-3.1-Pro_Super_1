using System.Collections.Generic;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public class MaxLinesRule : IQcRule
{
    private readonly int _threshold;
    public string Name => "MaxLines";

    public MaxLinesRule(int threshold)
    {
        _threshold = threshold;
    }

    public IEnumerable<QcResult> Execute(IEnumerable<Cue> cues)
    {
        foreach (var cue in cues)
        {
            if (cue.Lines.Count > _threshold)
            {
                yield return new QcResult(Name, cue.Id, QcStatus.Failed, $"Lines exceed {_threshold}");
            }
            else
            {
                yield return new QcResult(Name, cue.Id, QcStatus.Passed);
            }
        }
    }
}