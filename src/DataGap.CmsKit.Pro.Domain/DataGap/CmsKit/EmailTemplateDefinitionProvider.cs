using DataGap.Jellog.Emailing.Templates;
using DataGap.Jellog.TextTemplating;
using DataGap.CmsKit.Templates;

namespace DataGap.CmsKit;

public class EmailTemplateDefinitionProvider : TemplateDefinitionProvider
{
    public override void Define(ITemplateDefinitionContext context)
    {
        context.Add(
            new TemplateDefinition(
                    CmsKitEmailTemplates.ContactEmailTemplate,
                    layout: StandardEmailTemplates.Layout
                )
                .WithVirtualFilePath(
                    "/DataGap/CmsKit/Templates/ContactEmail.tpl",
                    isInlineLocalized: true
                )
        );
    }
}
