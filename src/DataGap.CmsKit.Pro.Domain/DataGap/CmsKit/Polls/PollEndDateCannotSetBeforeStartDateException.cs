using System;
using JetBrains.Annotations;
using DataGap.Jellog;

namespace DataGap.CmsKit.Polls;

[Serializable]
public class PollEndDateCannotSetBeforeStartDateException : BusinessException
{
    public PollEndDateCannotSetBeforeStartDateException([NotNull] DateTime startDate, [NotNull] DateTime endDate)
    : base(CmsKitProErrorCodes.Poll.PollEndDateCannotSetBeforeStartDateException)
    {
        WithData(nameof(Poll.StartDate), startDate);
        WithData(nameof(Poll.EndDate), endDate);
    }
}
