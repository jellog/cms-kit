using System;

namespace DataGap.CmsKit.Public.Polls;

[Serializable]
public class SubmitPollInput
{
    public Guid[] PollOptionIds { get; set; }
}
