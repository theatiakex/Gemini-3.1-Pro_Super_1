using System;
using System.Collections.Generic;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public class MinDurationRule : IQcRule
{
    private readonly TimeSpan _threshold;
    public string Name => "MinDuration";

    public MinDurationRule(TimeSpan threshold)
    {
        _threshold = threshold;
    }

    public IEnumerable<QcResult> Execute(IEnumerable<Cue> cues)
    {
        foreach (var cue in cues)
        {
            if (cue.End - cue.Start < _threshold)
            {
                yield return new QcResult(Name, cue.Id, QcStatus.Failed);
            }
            else
            {
                yield return new QcResult(Name, cue.Id, QcStatus.Passed);
            }
        }
    }
}