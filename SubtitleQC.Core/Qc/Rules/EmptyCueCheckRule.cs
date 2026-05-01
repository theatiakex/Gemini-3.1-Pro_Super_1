using System.Collections.Generic;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public class EmptyCueCheckRule : IQcRule
{
    public string Name => "EmptyCueCheck";

    public IEnumerable<QcResult> Execute(IEnumerable<Cue> cues)
    {
        foreach (var cue in cues)
        {
            bool hasContent = false;
            foreach (var line in cue.Lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    hasContent = true;
                    break;
                }
            }

            if (!hasContent)
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