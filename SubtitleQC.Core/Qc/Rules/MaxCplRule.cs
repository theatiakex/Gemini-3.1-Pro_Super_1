using System.Collections.Generic;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public class MaxCplRule : IQcRule
{
    private readonly int _threshold;
    public string Name => "MaxCpl";

    public MaxCplRule(int threshold)
    {
        _threshold = threshold;
    }

    public IEnumerable<QcResult> Execute(IEnumerable<Cue> cues)
    {
        foreach (var cue in cues)
        {
            bool failed = false;
            foreach (var line in cue.Lines)
            {
                if (line.Length > _threshold)
                {
                    failed = true;
                    break;
                }
            }
            
            if (failed)
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