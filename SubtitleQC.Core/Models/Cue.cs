using System;
using System.Collections.Generic;

namespace SubtitleQc.Core.Models;

public class Cue
{
    public string Id { get; }
    public TimeSpan Start { get; }
    public TimeSpan End { get; }
    public IReadOnlyList<string> Lines { get; }
    public int StartFrame { get; }
    public int EndFrame { get; }

    public Cue(string id, TimeSpan start, TimeSpan end, IReadOnlyList<string> lines, int startFrame = 0, int endFrame = 0)
    {
        Id = id;
        Start = start;
        End = end;
        Lines = lines;
        StartFrame = startFrame;
        EndFrame = endFrame;
    }
}