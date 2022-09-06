using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGap.Jellog.GlobalFeatures;
using DataGap.Jellog.Threading;

namespace DataGap.CmsKit.Pro;

public class FeatureConfigurer
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            GlobalFeatureManager.Instance.Modules.CmsKit().EnableAll();
            GlobalFeatureManager.Instance.Modules.CmsKitPro().EnableAll();
        });
    }
}
