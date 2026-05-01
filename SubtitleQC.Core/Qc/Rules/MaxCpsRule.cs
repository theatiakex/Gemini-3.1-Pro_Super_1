using System.Collections.Generic;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public class MaxCpsRule : IQcRule
{
    private readonly double _threshold;
    public string Name => "MaxCps";

    public MaxCpsRule(double threshold)
    {
        _threshold = threshold;
    }

    public IEnumerable<QcResult> Execute(IEnumerable<Cue> cues)
    {
        foreach (var cue in cues)
        {
            double durationSeconds = (cue.End - cue.Start).TotalSeconds;
            if (durationSeconds <= 0)
            {
                yield return new QcResult(Name, cue.Id, QcStatus.Passed);
                continue;
            }

            int totalChars = 0;
            foreach (var line in cue.Lines)
            {
                totalChars += line.Length;
            }

            double cps = totalChars / durationSeconds;
            if (cps > _threshold)
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