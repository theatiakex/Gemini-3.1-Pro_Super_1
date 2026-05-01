using System.Collections.Generic;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public class CrossShotBoundaryCheckRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;
    public string Name => "CrossShotBoundaryCheck";

    public CrossShotBoundaryCheckRule(IShotChangeProvider shotChangeProvider)
    {
        _shotChangeProvider = shotChangeProvider;
    }

    public IEnumerable<QcResult> Execute(IEnumerable<Cue> cues)
    {
        var cuts = _shotChangeProvider.GetShotChangeTimestamps();

        foreach (var cue in cues)
        {
            bool failed = false;
            foreach (var cut in cuts)
            {
                if (cut > cue.Start && cut < cue.End)
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