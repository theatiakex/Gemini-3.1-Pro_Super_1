namespace SubtitleQc.Core.Qc;

public class QcResult
{
    public string RuleName { get; }
    public string CueId { get; }
    public QcStatus Status { get; }
    public string Message { get; }

    public QcResult(string ruleName, string cueId, QcStatus status, string message = "")
    {
        RuleName = ruleName;
        CueId = cueId;
        Status = status;
        Message = message;
    }
}