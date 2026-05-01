using System;
using System.Collections.Generic;
using SubtitleQc.Core.Models;
using SubtitleQc.Core.Qc.Abstractions;

namespace SubtitleQc.Core.Qc.Rules;

public class MinFramesFromShotChangeRule : IQcRule
{
    private readonly IShotChangeProvider _shotChangeProvider;
    private readonly int _thresholdFrames;
    public string Name => "MinFramesFromShotChange";

    public MinFramesFromShotChangeRule(IShotChangeProvider shotChangeProvider, int thresholdFrames)
    {
        _shotChangeProvider = shotChangeProvider;
        _thresholdFrames = thresholdFrames;
    }

    public IEnumerable<QcResult> Execute(IEnumerable<Cue> cues)
    {
        var cuts = _shotChangeProvider.GetShotChangeFrames();

        foreach (var cue in cues)
        {
            bool failed = false;
            foreach (var cut in cuts)
            {
                int framesDiff = Math.Abs(cue.StartFrame - cut);
                // "a cue starts too close to a cut"
                // Actually, the spec says "cue starts too close to a cut". Let's check distance to startFrame.
                if (framesDiff > 0 && framesDiff < _thresholdFrames)
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