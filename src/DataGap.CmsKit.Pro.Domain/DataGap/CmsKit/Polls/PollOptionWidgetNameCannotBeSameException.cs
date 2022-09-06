using System;
using JetBrains.Annotations;
using DataGap.Jellog;

namespace DataGap.CmsKit.Polls;

[Serializable]
public class PollOptionWidgetNameCannotBeSameException : BusinessException
{
    public PollOptionWidgetNameCannotBeSameException([NotNull] string name)
    : base(CmsKitProErrorCodes.Poll.PollOptionWidgetNameCannotBeSame)
    {
        WithData(nameof(Poll.Widget), name);
    }
}
