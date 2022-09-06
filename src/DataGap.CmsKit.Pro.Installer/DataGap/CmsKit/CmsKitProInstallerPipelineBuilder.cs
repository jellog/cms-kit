using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DataGap.Jellog.DependencyInjection;
using DataGap.Jellog.Studio.ModuleInstalling;

namespace DataGap.CmsKit;

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IModuleInstallingPipelineBuilder))]
public class CmsKitProInstallerPipelineBuilder : ModuleInstallingPipelineBuilderBase, IModuleInstallingPipelineBuilder, ITransientDependency
{
    public async Task<ModuleInstallingPipeline> BuildAsync(ModuleInstallingContext context)
    {
        context.AddEfCoreConfigurationMethodDeclaration(
            new EfCoreConfigurationMethodDeclaration(
                "DataGap.CmsKit.EntityFrameworkCore",
                "ConfigureCmsKitPro"
            )
        );

        return GetBasePipeline(context);
    }
}
