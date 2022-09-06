using System;
using DataGap.CmsKit.EntityFrameworkCore;

namespace DataGap.CmsKit.Pro;

public abstract class CmsKitProDomainTestBase : CmsKitProTestBase<CmsKitProDomainTestModule>
{
    protected virtual void UsingDbContext(Action<ICmsKitDbContext> action)
    {
        using (var dbContext = GetRequiredService<ICmsKitDbContext>())
        {
            action.Invoke(dbContext);
        }
    }

    protected virtual T UsingDbContext<T>(Func<ICmsKitDbContext, T> action)
    {
        using (var dbContext = GetRequiredService<ICmsKitDbContext>())
        {
            return action.Invoke(dbContext);
        }
    }
}
