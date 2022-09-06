using DataGap.Jellog.Emailing.Templates;
using DataGap.Jellog.TextTemplating;
using DataGap.CmsKit.Localization;

namespace DataGap.CmsKit.Public.Emailing.Templates;

public class CmsKitEmailTemplateDefinitionProvider : TemplateDefinitionProvider
{
    public override void Define(ITemplateDefinitionContext context)
    {

        context.Add(
            new TemplateDefinition(
                CmsKitEmailTemplates.NewsletterEmailTemplate,
                localizationResource: typeof(CmsKitResource),
                layout: StandardEmailTemplates.Layout
            ).WithVirtualFilePath("/DataGap/CmsKit/Public/Emailing/Templates/NewsletterEmailLayout.tpl", true)
        );
    }
}
