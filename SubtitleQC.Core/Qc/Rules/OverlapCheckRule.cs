using System.Collections.Generic;
using System.Linq;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public class OverlapCheckRule : IQcRule
{
    public string Name => "OverlapCheck";

    public IEnumerable<QcResult> Execute(IEnumerable<Cue> cues)
    {
        var cueList = cues.OrderBy(c => c.Start).ToList();

        for (int i = 0; i < cueList.Count; i++)
        {
            if (i == 0)
            {
                yield return new QcResult(Name, cueList[i].Id, QcStatus.Passed);
                continue;
            }

            var currentCue = cueList[i];
            var previousCue = cueList[i - 1];

            if (currentCue.Start < previousCue.End)
            {
                yield return new QcResult(Name, currentCue.Id, QcStatus.Failed);
            }
            else
            {
                yield return new QcResult(Name, currentCue.Id, QcStatus.Passed);
            }
        }
    }
}