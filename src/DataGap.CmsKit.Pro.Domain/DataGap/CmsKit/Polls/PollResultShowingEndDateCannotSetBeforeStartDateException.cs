﻿using System;
using JetBrains.Annotations;
using DataGap.Jellog;

namespace DataGap.CmsKit.Polls;

[Serializable]
public class PollResultShowingEndDateCannotSetBeforeStartDateException : BusinessException
{
    public PollResultShowingEndDateCannotSetBeforeStartDateException([NotNull] DateTime startDate, [NotNull] DateTime resultShowingEndDate)
    : base(CmsKitProErrorCodes.Poll.PollResultShowingEndDateCannotSetBeforeStartDate)
    {
        WithData(nameof(Poll.StartDate), startDate);
        WithData(nameof(Poll.ResultShowingEndDate), resultShowingEndDate);
    }
}
