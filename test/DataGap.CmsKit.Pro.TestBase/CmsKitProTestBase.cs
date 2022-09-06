using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog;
using DataGap.Jellog.Modularity;
using DataGap.Jellog.Uow;
using DataGap.Jellog.Testing;

namespace DataGap.CmsKit.Pro;

/* All test classes are derived from this class, directly or indirectly. */
public abstract class CmsKitProTestBase<TStartupModule> : JellogIntegratedTest<TStartupModule>
    where TStartupModule : IJellogModule
{
    protected override void SetJellogApplicationCreationOptions(JellogApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    protected virtual Task WithUnitOfWorkAsync(Func<Task> func)
    {
        return WithUnitOfWorkAsync(new JellogUnitOfWorkOptions(), func);
    }

    protected virtual async Task WithUnitOfWorkAsync(JellogUnitOfWorkOptions options, Func<Task> action)
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin(options))
            {
                await action();

                await uow.CompleteAsync();
            }
        }
    }

    protected virtual Task<TResult> WithUnitOfWorkAsync<TResult>(Func<Task<TResult>> func)
    {
        return WithUnitOfWorkAsync(new JellogUnitOfWorkOptions(), func);
    }

    protected virtual async Task<TResult> WithUnitOfWorkAsync<TResult>(JellogUnitOfWorkOptions options, Func<Task<TResult>> func)
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin(options))
            {
                var result = await func();
                await uow.CompleteAsync();
                return result;
            }
        }
    }
}
